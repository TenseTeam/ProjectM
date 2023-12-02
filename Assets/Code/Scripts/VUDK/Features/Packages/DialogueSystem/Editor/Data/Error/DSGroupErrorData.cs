namespace VUDK.Features.Packages.DialogueSystem.Editor.Data.Error
{
    using System.Collections.Generic;
    using VUDK.Features.Packages.DialogueSystem.Editor.Elements;

    public class DSGroupErrorData
    {
        public DSErrorData ErrorData;

        public List<DSGroup> Groups { get; private set; }

        public DSGroupErrorData()
        {
            ErrorData = new DSErrorData();
            Groups = new List<DSGroup>();
        }
    }
}
