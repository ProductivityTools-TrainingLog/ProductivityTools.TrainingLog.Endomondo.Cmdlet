using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.TrainingLog.Endomondo.ImportTrainings
{
    [Cmdlet("Import", "EndomondoTrainingsToTrainingLog")]
    public class ImportTrainingsCmdlet: PSCmdlet.PSCmdletPT
    {
        [Parameter]
        public string Path { get; set; }

        [Parameter]
        public string Account { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Hello! Let us get to work!");
            App application = new App(this.Path, this.Account);
            application.Import();
            base.ProcessRecord();
        }
    }
}
