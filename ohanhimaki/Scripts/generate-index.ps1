$blogDir = "../ohanhimaki.Web/wwwroot/blog"
$indexFile = "$blogDir/index.json"
$posts = @()

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

$posts | Sort-Object -Property date -Descending | ConvertTo-Json -Depth 10 | Set-Content $indexFile

echo "Index generated"
echo $indexFile
echo $posts
