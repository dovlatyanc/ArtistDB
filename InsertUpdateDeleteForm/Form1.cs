using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InsertUpdateDeleteForm
{
    public partial class Form1 : Form
    {
        DB db = new DB();
        public Form1()
        {
            InitializeComponent();
            
        }



        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listArtistsName.SelectedItems.Count != 1)
            {
                return;
            }
            int index = listArtistsName.SelectedIndices[0];
            ListViewItem selectedItem = listArtistsName.Items[index];

            //int Id = (int)listArtistsName.SelectedItem.Tag!;


            Artist selectedArtist = (Artist)listArtistsName.SelectedItems[0].Tag;
            int Id = selectedArtist.Id;

            db.Update(textBoxName.Text, Id);
        }

        private void buttonConnection_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listArtistsName.Items.Clear();

            foreach (var artist in db.ReadDB()) {
                ListViewItem item = new ListViewItem(artist.Name);
                item.Tag = artist;
                listArtistsName.Items.Add(item);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            db.Insert(textBoxName.Text);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listArtistsName.SelectedItems.Count != 1)
            {
                return;
            }
            int index = listArtistsName.SelectedIndices[0];
            ListViewItem selectedItem = listArtistsName.Items[index];


            Artist selectedArtist = (Artist)listArtistsName.SelectedItems[0].Tag;
            int Id = selectedArtist.Id;

            db.Delete(Id);
        }
    }
}
