param (
[string]$OutputDir,
[string]$BaseDir,
[string]$TargetFileName,
[string]$PublishPath,
[string]$ConfuserConfigurationFile,
[string]$ConfuserPath,
[string]$Preset
)

function replace-file-content([string] $path, [string] $replace, [string] $replaceWith)
{
(Get-Content $path) |
Foreach-Object {$_ -replace $replace,$replaceWith}|
Out-File $path
}

$OutputDir = $OutputDir.Substring(1,$OutputDir.Length-2)
$BaseDir = $BaseDir.Substring(1,$BaseDir.Length-2)
$PublishPath = $PublishPath.Substring(1,$PublishPath.Length-2)
$TargetFileName = $TargetFileName.Substring(1,$TargetFileName.Length-2)
$ConfuserConfigurationFile = $ConfuserConfigurationFile.Substring(1,$ConfuserConfigurationFile.Length-2)
$ConfuserPath = $ConfuserPath.Substring(1,$ConfuserPath.Length-2)
$Preset = $Preset.Substring(1,$Preset.Length-2)

echo "OutputDir:" $OutputDir
echo "BaseDir:" $BaseDir
echo "TargetFileName:" $TargetFileName
echo "ConfuserConfigurationFile:" $ConfuserConfigurationFile

# Let's first copy the configuration file to a temporary directory
echo "Obfuscating..."
$tempFile = [string]::Concat($BaseDir, "confuser.temp.crproj")
echo "Copying " $ConfuserConfigurationFile " to " $tempFile
Copy-Item $ConfuserConfigurationFile -Destination $tempFile
echo "Replacing placeholders..."
replace-file-content $tempFile "{OutputDir}" $OutputDir
replace-file-content $tempFile "{BaseDir}" $BaseDir
replace-file-content $tempFile "{TargetFileName}" $TargetFileName
replace-file-content $tempFile "{Preset}" $Preset
echo "Performing Obfuscation..."
$parameter = [string]::Concat("""",$tempFile,"""")
$ConfuserPath = [string]::Concat("""",$ConfuserPath,"""")
echo $parameter
#start-process -wait "C:\Programs\ConfuserEx\Confuser.CLI.exe" "$parameter"
start-process -wait "$ConfuserPath" "$parameter"
echo "Obfuscation complete."

if([System.IO.File]::Exists($PublishPath)){
    # file with path $path exists. Replace with obfuscated exe
	echo "Copying exe to publish path"
	Copy-Item $TargetFileName -Destination $PublishPath
}

echo "Removing " $tempFile
#Remove-Item $tempFile
echo "Done!!!"