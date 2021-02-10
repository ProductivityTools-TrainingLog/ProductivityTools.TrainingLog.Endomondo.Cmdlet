using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.TrainingLog.Endomondo.ImportTrainings
{
    [Cmdlet("Import", "EndomondoTrainingsToTrainingLog")]
    public class ImportTrainingsCmdlet: PSCmdlet.PSCmdletPT
    {
        [Parameter(Mandatory = true, HelpMessage = "Path to Endomondo backup directory, it should be path to main directory not to WorkOuts subdirectory")]
        public string Path { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "Unique string which will point out account. In my case it was my email address.")]
        public string Account { get; set; }

        [Parameter(Mandatory =true,HelpMessage ="Address of the TrainingLog API.")]
        public string TrainingLogApiAddress { get; set; }

        protected override void ProcessRecord()
        {
            WriteVerbose("Hello! Let us get to work!");
            App application = new App(this.Path, this.Account, this.TrainingLogApiAddress);
            application.Import();
            base.ProcessRecord();
            WriteVerbose("I hope I helped, bye!");
        }
    }
}
