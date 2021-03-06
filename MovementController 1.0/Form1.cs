using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovementController_1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SlewButton_Click(object sender, EventArgs e)
        {
            decimal endEL = ELPositionInput.Value;
            decimal endAZ = AZPositionInput.Value;
            DateTime arrivalTime = ArrivalTimeInput.Value;

            SlewInstruction inputInstruction = new SlewInstruction(endAZ, endEL, arrivalTime);
            Graph(inputInstruction);
        }

        private void TrackButton_Click(object sender, EventArgs e)
        {
            decimal endEL = ELPositionInput.Value;
            decimal endAZ = AZPositionInput.Value;
            DateTime arrivalTime = ArrivalTimeInput.Value;

            TrackInstruction inputInstruction = new TrackInstruction(endAZ, endEL, arrivalTime);
            Graph(inputInstruction);
        }

        private void DriftScanButton_Click(object sender, EventArgs e)
        {
            decimal endEL = ELPositionInput.Value;
            decimal endAZ = AZPositionInput.Value;
            DateTime arrivalTime = ArrivalTimeInput.Value;

            DriftScanInstruction inputInstruction = new DriftScanInstruction(endAZ, endEL, arrivalTime);
            Graph(inputInstruction);
        }

        private void Graph(Instruction instruction)
        {
            InstructionInterpreter instructionInterpreter = new InstructionInterpreter();
            instructionInterpreter.ProcessInstructionInput(instruction);

            // Graph Elevation vs. Time 
            this.ELChart.Series[0].Points.Clear();
            foreach (var cmd in instructionInterpreter.trajectory)
            {
                this.ELChart.Series[0].Points.AddXY(cmd.diffSecs, cmd.coordinates.elevation);
            }

            // Graph Azimuth vs. Time 
            this.AZChart.Series[0].Points.Clear();
            foreach (var cmd in instructionInterpreter.trajectory)
            {
                this.AZChart.Series[0].Points.AddXY(cmd.diffSecs, cmd.coordinates.azimuth);
            }

            // Graph Elevation vs. Azimuth
            this.ELAZChart.Series[0].Points.Clear();
            foreach (var cmd in instructionInterpreter.trajectory)
            {
                this.ELAZChart.Series[0].Points.AddXY(cmd.coordinates.azimuth, cmd.coordinates.elevation);
            }
        }

        
    }
}
