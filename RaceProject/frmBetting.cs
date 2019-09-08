using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RaceProject
{
    public partial class frmBetting : Form
    {
        public frmBetting()
        {
            InitializeComponent();
        }
        // codding for structure of the project//

        private Guy []_listOfGuys = null;
        private GreyHound []_listOfDogs = null;
        private int _flag = 0;
        private bool _enableRaceBtn = false;
        
        public void FillArrays()
        {
            Random myRandom = new Random();

            _listOfGuys = new Guy[3]
            {
                // codding for 1st player //
                new Guy() 
                { 
                    Name = "Rony", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = lblGuy1, 
                    MyRadioButton = rdbGuy1
                },
                // codding for 2nd race player //

                new Guy()
                { 
                    // codding for 3rd race player //
                    Name = "Deep", 
                    Cash = 50,  
                    MyBet = new Bet(),  
                    MyLabel = lblGuy2, 
                    MyRadioButton = rdbGuy2
                },

                new Guy() 
                { 
                    // codding for 4th race player //
                    Name = "Jass", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = lblGuy3, 
                    MyRadioButton = rdbGuy3
                }
            };
            // codding for dog race track //

            _listOfDogs = new GreyHound[4]
            {
                // race dog 1st codding //
                new GreyHound() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70, 
                    StartingPosition = pBoxRaceTrack.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = pbDog1
                },
                // race dog 2nd codding //
                new GreyHound()
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70, 
                    StartingPosition = pBoxRaceTrack.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = pbDog2
                },
                // race dog 3rd codding //
                new GreyHound() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70, 
                    StartingPosition = pBoxRaceTrack.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = pbDog3
                },
                // race dog 4th codding //
                new GreyHound() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70, 
                    StartingPosition = pBoxRaceTrack.Location.X, 
                    MyRandom = myRandom, 
                    MyPictureBox = pbDog4
                }
            };

            for (int i = 0; i < _listOfGuys.Length; i++)
            {
                _listOfGuys[i].MyBet.Bettor = _listOfGuys[i];
                _listOfGuys[i].UpdateLabels();
            }

            PlaceDogPicturesAtStart();            
        }

        private void frmBetting_Load(object sender, EventArgs e)
        {
            // codding for betting limit //
            try
            {
                if (numBucks.Value == 5)
                    lblMinimumBet.Text = "Minimum limit : 1 Dollar";

                FillArrays();
                
                if (!this._enableRaceBtn)
                    btnRace.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // coding for race of dogs finish flags//

        private void rdbGuy1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGuy1.Checked)
            {
                this._flag = 1;

                lblGuyName.Text = this._listOfGuys[0].Name;
            }
        }

        private void rdbGuy2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGuy2.Checked)
            {
                this._flag = 2;
                lblGuyName.Text = this._listOfGuys[1].Name;
            }
        }

        private void rdbGuy3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGuy3.Checked)
            {
                this._flag = 3;
                lblGuyName.Text = this._listOfGuys[2].Name;
            }
        }

        public void BetsButtonWorking()
        {            
            int bucksNumber = 0;
            int dogNumber = 0;

            if (!rdbGuy1.Checked && !rdbGuy2.Checked && !rdbGuy3.Checked)
            {
                MessageBox.Show("You must choose atleast one guy to place bet.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bucksNumber = Convert.ToInt32(numBucks.Value);
            dogNumber = Convert.ToInt32(numDogNo.Value);

            if (IsExceedBetLimit(bucksNumber))
            {
                MessageBox.Show("You can't put Dollar greater than 45 on dog.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _enableRaceBtn = true; // if at least one bet is placed enable race button then

            if (this._flag == 1)
            {
                this._listOfGuys[0].PlaceBet(bucksNumber, dogNumber);
            }
            else if (this._flag == 2)
            {
                this._listOfGuys[1].PlaceBet(bucksNumber, dogNumber);
            }
            else if (this._flag == 3)
            {
                this._listOfGuys[2].PlaceBet(bucksNumber, dogNumber);
            }            
        }

        private void btnBets_Click(object sender, EventArgs e)
        {
            try
            {
                BetsButtonWorking();

                if (this._enableRaceBtn)
                    btnRace.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool IsExceedBetLimit(int amount)
        {
            if (amount > 45)
                return true;

            return false;
        }

        public void RaceButtonWorking()
        {
            btnBets.Enabled = false;
            btnRace.Enabled = false;

            bool winnerDogFlag = false;
            int winningDogNo = 0;            

            while (!winnerDogFlag)
            {
                for (int i = 0; i < _listOfDogs.Length; i++)
                {
                    if (this._listOfDogs[i].Run())
                    {
                        winnerDogFlag = true;
                        winningDogNo = i;
                    }
                  
                    pBoxRaceTrack.Refresh();                 
                }                
            }

            MessageBox.Show("We have a winner - dog # " + (winningDogNo + 1) + "!", "Race Over");

            for (int j = 0; j < _listOfGuys.Length; j++)
            {
                this._listOfGuys[j].Collect(winningDogNo + 1);
                this._listOfGuys[j].ClearBet(); // clearing all bets
            }

            PlaceDogPicturesAtStart();

            btnBets.Enabled = true;       
        }

        public void PlaceDogPicturesAtStart()
        {
            for (int k = 0; k < _listOfDogs.Length; k++)
                _listOfDogs[k].TakeStartingPosition();  
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            try
                // racebutton working //
            {
                RaceButtonWorking();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PbDog3_Click(object sender, EventArgs e)
        {

        }

        private void NumBucks_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PBoxRaceTrack_Click(object sender, EventArgs e)
        {

        }
    }
}
