﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicRandomizer
{
    public partial class NewPlaylistForm : Form
    {
        public String name;

        public NewPlaylistForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           name = txtName.Text;

            // Check to make sure the user actually entered a name
            if (name.Length == 0)
            {
                MessageBox.Show("Please enter in a name.");
                return;
            }

            // Check for invalid characters in the filename
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (name.Contains(c))
                {
                    MessageBox.Show("There are invalid characters in the playlist name.");
                    return;
                }
            }

            // Check to make sure this name isn't already taken
            String[] playlists = Directory.GetFiles("playlists");
            foreach (String playlist in playlists)
            {
                if (playlist.Equals(name))
                {
                    MessageBox.Show("This playlist already exists.");
                    return;
                }
            }

            // Create the playlist
            using (FileStream writer = File.OpenWrite("playlists\\" + name + ".xml"))
            {
                MainForm.serializer.Serialize(writer, new List<MusicFile>());
            }

            this.Close();
        }

    }
}
