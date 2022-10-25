using System;

namespace FontInstaller
{
    [Serializable]
    public class FontAlreadyExistsException : Exception
    {
        public FontAlreadyExistsException() { }


        public FontAlreadyExistsException(string fontFilePath)
            : base(String.Format("Font already exists: {0}", fontFilePath))
        {

        }
    }
}
