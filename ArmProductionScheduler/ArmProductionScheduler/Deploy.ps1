<# To use in visual studio modify the following code based on confiruation:

######## NOTES #########
##1.Need quotes around the projectDirectory in case of spaces.
##2.Need quote and single quote round -File and -appName parameters in case of spaces.
######## NOTES #########

if $(ConfigurationName)==Release_Test (
powershell.exe -ExecutionPolicy Bypass -File "$(ProjectDir)Deploy.ps1" -binDirectory "'$(TargetDir)'" -appName "'$(ProjectName)'" -env Test
)
if $(ConfigurationName)==Release_Dev (
powershell.exe -ExecutionPolicy Bypass -File "$(ProjectDir)Deploy.ps1" -binDirectory "'$(TargetDir)'" -appName "'$(ProjectName)'" -env Dev
)
if $(ConfigurationName)==Release_Upgrade (
powershell.exe -ExecutionPolicy Bypass -File "$(ProjectDir)Deploy.ps1" -binDirectory "'$(TargetDir)'" -appName "'$(ProjectName)'" -env Upgrade
)
if $(ConfigurationName)==Release_Live (
powershell.exe -ExecutionPolicy Bypass -File "$(ProjectDir)Deploy.ps1" -binDirectory "'$(TargetDir)'" -appName "'$(ProjectName)'" -env Live
)
#>
param
(
    [string]$binDirectory = "D:\delete\App"
    ,[string]$appName = "Test"
    ,[string]$env = "Test"
)

#Clean parameters that have single quote surrounding string.  Need to clean before setting $DeployDirectories.
if($binDirectory.StartsWith("'") -and $binDirectory.EndsWith("'"))
{
    $binDirectory = $binDirectory.Substring(1,$binDirectory.Length - 2)
}
if($appName.StartsWith("'") -and $appName.EndsWith("'"))
{
    $appName = $appName.Substring(1,$appName.Length - 2)
}

<############################################################## TODO: Add deployment directories to copy files to and daysToKeep ###################################################################>
[System.Collections.ArrayList]$DeployDirectories = [System.Collections.ArrayList]@()
[int] $daysToKeep = -1

if($env -eq "Live")
{
    $DeployDirectories.Add("\\iasc-rds-s1\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s2\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s3\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-sh4\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-sh5\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s6\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s7\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s8\C$\SabreExe\Live\$appName")
    $DeployDirectories.Add("\\iasc-rds-s9\C$\SabreExe\Live\$appName")
}
elseif($env -eq "Test")
{
    $DeployDirectories.Add("\\iasc-rds-s1\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-s2\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-s3\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-sh4\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-sh5\C$\SabreExe\Test\$appName") 
    $DeployDirectories.Add("\\iasc-rds-s6\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-s7\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-s8\C$\SabreExe\Test\$appName")
    $DeployDirectories.Add("\\iasc-rds-s9\C$\SabreExe\Test\$appName")
}
elseif($env -eq "Dev")
{
    $DeployDirectories.Add("\\Iasc-dev-rds01.sabre.local\c$\SabreExe\Dev\$appName")
}
elseif($env -eq "Upgrade")
{
	$DeployDirectories.Add("\\IASC-RDS-S1\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S2\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S3\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S4\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S5\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S6\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S7\C$\SabreExe\FWTIntegration\$appName")
	$DeployDirectories.Add("\\IASC-RDS-S8\C$\SabreExe\FWTIntegration\$appName")
}


###### Variables #####
[string[]]$files
[datetime] $currentDate = get-date
[string] $earliestJulianDate
[string] $curDeployDirectory
[string] $deployFile
[string] $statusMessage = ""

$earliestJulianDate = $currentDate.AddDays($daysToKeep).ToString("yyyyMMdd")
$todayJulianDate = $currentDate.ToString("yyyyMMdd")

write-host "EarliestJulianDate = $earliestJulianDate"

$statusMessage = "Running for environment $env"
$statusMessage = $statusMessage + "`nBin  directory $binDirectory"

if($binDirectory.Substring($binDirectory.Length - 1) -ne "\")
{
    $binDirectory = $binDirectory + "\"
}

try
{
    for($iDir=0; $iDir -lt $DeployDirectories.Count; $iDir++)
    {
        $curDeployDirectory = $DeployDirectories[$iDir]
        $statusmessage = $statusmessage + "`n" + "Copying to directory $curDeployDirectory"
        write-host "Checking if directory $curDeployDirectory exists..."
        <############################################################## Step 1: Checking that directory exists ###################################################################>
        if(!(Test-Path -path $curDeployDirectory))
        {
            write-host "Creating directory $curDeployDirectory..."
            New-Item -ItemType directory -Path $curDeployDirectory
            $statusmessage = $statusmessage + "`nCreated directory $curDeployDirectory"
        }
        <############################################################## Step 2: Clean deployment folders ###################################################################>
        if($curDeployDirectory.Substring($curDeployDirectory.Length -1) -ne "\")
        {
            $curDeployDirectory = $curDeployDirectory + "\"
        }
        
        write-host "Cleaning deployment directory $curDeployDirectory..."
        $files = Get-ChildItem $curDeployDirectory -ErrorAction Stop

        for($iFiles=0; $iFiles -lt $files.Count; $iFiles++)
        {
            $deployFile = $files[$iFiles]
            write-host "Deploy file = $deployFile.FullName"
            if($deployFile.Name.Length -gt 7)
            {
                write-host "$deployFile is greater than 7"
                if($deployFile.Name.Substring(0,8) -match "^\d{8}")
                {
                    write-host "$deployFile.Name starts with 8 digits"
                    if($deployFile.Name.Substring(0,8) -le $earliestJulianDate -or $deployFile.Name.Substring(0,8) -eq $todayJulianDate)
                    {
                        $deleteFile = $deployFile.FullName
                        write-host "Deleting file $deleteFile"
                        Remove-Item $deleteFile -Recurse
                    }
                }
            }
        }

        $statusmessage = $statusmessage + "`nFinished cleaning directory $curDeployDirectory"
        <############################################################## Step 3: Rename files to copy ###################################################################>
        [bool]$rename = 1
        for($iFiles=0; $iFiles -lt $files.Count; $iFiles++)
        {
            $rename = 1
            $deployFile = $files[$iFiles]
            write-host "Deploy file = $deployFile"
            if($deployFile.ToString().Length -gt 13)
            {
                #dont need to rename file if already have been renamed
                if($deployFile.ToString().Substring(0,14) -match "^\d{14}")
                {
                    $rename = 0
                }
            }
            if($rename)
            {
                $renameFile = $deployFile.FullName
                $newNameFile = -join($currentDate.ToString("yyyyMMddhhmmss"), "_", $deployFile)
                write-host "Renaming $renameFile to $newNameFile"
                Rename-Item -Path $renameFile -NewName $newNameFile -ErrorAction Stop
            }
        }
        $statusmessage = $statusmessage + "`nRename of files complete"
        <############################################################## Step 4: Copy files from bin directory ###################################################################>
        $binFiles = Get-ChildItem $binDirectory -Recurse
        write-host
        write-host "BinDirectory = $binDirectory"
        for($iBinFiles = 0; $iBinFiles -lt $binFiles.Count; $iBinFiles++)
        {
            $copyFullName = $binFiles[$iBinFiles].FullName
            $copyFileName = $binFiles[$iBinFiles].Name
            
            #Check if folder, if it is, copy all contents in folder
            if((Get-Item $copyFullName) -is [System.IO.DirectoryInfo])
            {
                #Make sure the parent folder is the BinDirectory and not a subfolder of a folder within the binDirectory
                if((get-item $copyFullname).Parent.FullName + "\" -eq $binDirectory)
                {
                    write-host "Copy $copyFullName"
                    copy-item $copyFullName -Destination $curDeployDirectory -ErrorAction Stop -Container -recurse
                }
            }
            else
            {
                #Make sure it's in the bin directory and not in a subfolder which was copied during the folder copy above
                if(-join($binDirectory,$copyFileName) -eq $copyFullName)
                {
                    write-host "Copy file $copyFullName to $curDeployDirectory"
                    copy-item $copyFullName -Destination $curDeployDirectory -ErrorAction Stop
                }
                
            }
        }
        $statusmessage = $statusmessage + "`nCopy of files into directory $curDeployDirectory complete"
    }
    $statusMessage = $statusMessage + "`nCompleted"
    $wshell = New-Object -ComObject Wscript.Shell
    $wshell.Popup($statusMessage,0,"Done",0x0)
}
Catch
{
    $errorMessage = $_.Exception.Message + "`n`n" + $statusmessage
    $wshell = New-Object -ComObject Wscript.Shell
    $wshell.Popup($errorMessage,0,"Error",0x0)
   
}