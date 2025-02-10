$sourceDir = "../../blog" # Source directory
$blogDir = "../ohanhimaki.Web/wwwroot/blog" # Destination directory
$indexFile = "$blogDir/index.json"
$posts = @()

# Ensure destination exists
if (!(Test-Path $blogDir)) {
    New-Item -ItemType Directory -Path $blogDir | Out-Null
}

# Copy all markdown files
Copy-Item -Path "$sourceDir/*.md" -Destination $blogDir -Force

# Process markdown files
Get-ChildItem -Path $blogDir -Filter "*.md" | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    if ($content -match "(?ms)^---\r?\n(.*?)\r?\n---\r?\n") {
        echo "Processing $($_.Name)"
        $yaml = $matches[1] | ConvertFrom-Yaml
        $posts += @{
            title = $yaml.title
            date = $yaml.date
            tags = $yaml.tags
            file = $_.Name
        }
    }
}

# Create index.json
$posts | Sort-Object -Property date -Descending | ConvertTo-Json -Depth 10 | Set-Content $indexFile

echo "Index generated"
echo $indexFile
echo $posts
