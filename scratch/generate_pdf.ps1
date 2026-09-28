Add-Type -AssemblyName System.Drawing
$htmlPath = "m:\Dev\UoVT\Group assignment\Group assignment\Docs\Documentation.html"
$html = Get-Content $htmlPath -Raw

$images = @("Screenshot 2026-09-28 232232.png", "Screenshot 2026-09-28 232307.png", "Screenshot 2026-09-28 233615.png", "Screenshot 2026-09-28 233452.png", "Screenshot 2026-09-28 233522.png", "Screenshot 2026-09-28 234951.png")

foreach ($img in $images) {
    $imgPath = "m:\Dev\UoVT\Group assignment\Group assignment\Screenshots\$img"
    $image = [System.Drawing.Image]::FromFile($imgPath)
    $w = $image.Width
    $h = $image.Height
    $image.Dispose()
    
    $newW = 600
    if ($w -gt 600) {
        $newH = [int]([double]$h * ([double]$newW / [double]$w))
    } else {
        $newW = $w
        $newH = $h
    }
    
    # We replace 'width="100%"' with explicitly calculated width and height
    # E.g. <img src="../Screenshots/Screenshot 2026-09-28 232232.png" width="100%" />
    $pattern = [regex]::Escape("<img src=`"../Screenshots/$img`" width=`"100%`" />")
    $replacement = "<img src=`"../Screenshots/$img`" width=`"$newW`" height=`"$newH`" />"
    $html = $html -replace $pattern, $replacement
}

Set-Content -Path $htmlPath -Value $html

$pdfPath = "m:\Dev\UoVT\Group assignment\Group assignment\Docs\Documentation.pdf"
Remove-Item -Path $pdfPath -ErrorAction SilentlyContinue

$word = New-Object -ComObject Word.Application
$doc = $word.Documents.Open($htmlPath)
$doc.SaveAs([ref]$pdfPath, [ref]17)
$doc.Close()
$word.Quit()
