function SetMsDeployPath()
{
    # Find latest version MSDeploy to use
    $ErrorActionPreference = "SilentlyContinue"
    for($i=4;$i -ge 1;$i--)
    {
        try
        {
            $RegKey = Get-ItemProperty -Path "Registry::HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\IIS Extensions\MSDeploy\$i\"
            if ($RegKey)
            {
                [System.Environment]::SetEnvironmentVariable("MSDeployPath", $RegKey.InstallPath)
                break
            }
        }
        catch
        {

        }
    }
    $ErrorActionPreference = "Continue"
}