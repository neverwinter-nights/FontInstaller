namespace FontInstaller
{
    public enum ExitCode : int
    {
        Success = 0,
        FontFileIsNotSpecified = 1,
        FontFileDoesNotExist = 2,
        FontInstallationError = 3
    }
}
