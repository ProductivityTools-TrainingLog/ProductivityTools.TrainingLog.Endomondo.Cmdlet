using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.TrainingLog.Endomondo.ImportTrainings
{
    [Cmdlet("Import", "Trainings")]
    public class ImportTrainingsCmdlet: PSCmdlet.PSCmdletPT
    {
        protected override void ProcessRecord()
        {
            Console.WriteLine("Hello");
            base.ProcessRecord();
        }
    }
}
