<#
.SYNOPSIS
    Asentaa central-context/root-ai-files -sisällön (AGENTS.md, skills/) aidrivenworkflow-kansion juureen
    symlinkkeinä, ja kloonaa (valinnaisesti) tiimin repot workflow-juureen.

.DESCRIPTION
    Oletusrakenne:
        aidrivenworkflow/
        ├── local-dev/
        ├── central-context/
        │   ├── setup.ps1          <- tämä skripti
        │   ├── root-ai-files/
        │   │   ├── AGENTS.md
        │   │   └── skills/
        │   └── repos.json          <- valinnainen, kloonattavien repojen lista
        ├── company-portal/
        └── finance/

    Skripti luo symlinkit workflow-juureen (yksi taso central-context:in yläpuolella):
        aidrivenworkflow/AGENTS.md      -> central-context/root-ai-files/AGENTS.md
        aidrivenworkflow/skills          -> central-context/root-ai-files/skills

    Symlinkin luonti vaatii Windowsilla joko admin-oikeudet tai Developer Mode päällä.
    Helpoin tapa: aja skripti "sudo pwsh -File ./setup.ps1" (Windows 11:n sisäänrakennettu
    sudo). Jos symlink ei silti onnistu, skripti fallbackaa tavalliseen kopiointiin ja
    varoittaa, että kopio ei pysy automaattisesti synkassa central-contextin kanssa.

.EXAMPLE
    cd aidrivenworkflow/central-context
    sudo pwsh -File ./setup.ps1

.EXAMPLE
    # myös repojen kloonaus repos.json:sta
    sudo pwsh -File ./setup.ps1 -CloneRepos
#>

[CmdletBinding()]
param(
    [switch]$CloneRepos,
    [switch]$Force
)

$ErrorActionPreference = "Stop"

$ContextDir = $PSScriptRoot
$WorkflowRoot = Split-Path -Parent $ContextDir
$RootAiFiles = Join-Path $ContextDir "root-ai-files"

function New-SymlinkOrCopy {
    param(
        [string]$SourcePath,
        [string]$TargetPath
    )

    if (Test-Path $TargetPath) {
        if (-not $Force) {
            Write-Warning "Skip: '$TargetPath' on jo olemassa. Käytä -Force ylikirjoittaaksesi."
            return
        }
        Remove-Item -Path $TargetPath -Recurse -Force
    }

    try {
        # New-Item -ItemType SymbolicLink toimii sekä tiedostoille että kansioille.
        New-Item -ItemType SymbolicLink -Path $TargetPath -Target $SourcePath | Out-Null
        Write-Host "OK  symlink: $TargetPath -> $SourcePath"
    }
    catch {
        Write-Warning "Symlink epäonnistui ($($_.Exception.Message)). Kopioidaan sen sijaan (ei pysy synkassa muutoksille)."
        Copy-Item -Path $SourcePath -Destination $TargetPath -Recurse -Force
        Write-Host "OK  kopio: $TargetPath (lähde: $SourcePath)"
    }
}

Write-Host "== AI-driven multirepo setup ==" -ForegroundColor Cyan
Write-Host "Workflow root: $WorkflowRoot"

# 1) AGENTS.md juureen
New-SymlinkOrCopy `
    -SourcePath (Join-Path $RootAiFiles "AGENTS.md") `
    -TargetPath (Join-Path $WorkflowRoot "AGENTS.md")

# 2) yhteiset skillsit juureen
New-SymlinkOrCopy `
    -SourcePath (Join-Path $RootAiFiles "skills") `
    -TargetPath (Join-Path $WorkflowRoot "skills")

# 3) valinnainen: kloonaa repos.json:issa listatut repot workflow-juureen
if ($CloneRepos) {
    $ReposFile = Join-Path $ContextDir "repos.json"
    if (-not (Test-Path $ReposFile)) {
        Write-Warning "repos.json puuttuu ($ReposFile), ohitetaan kloonaus."
    }
    else {
        $repos = Get-Content $ReposFile -Raw | ConvertFrom-Json
        foreach ($repo in $repos) {
            $dest = Join-Path $WorkflowRoot $repo.name
            if (Test-Path $dest) {
                Write-Host "Skip clone, kansio olemassa: $($repo.name)"
                continue
            }
            Write-Host "Kloonataan $($repo.name) <- $($repo.url)"
            git clone $repo.url $dest
        }
    }
}

Write-Host "Valmis." -ForegroundColor Green
