using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    public partial class ArtifactDetails : UserControl
    {
        public ArtifactDetails()
        {
            InitializeComponent();
        }

        public void ClearDisplay()
        {
            text_Set.Text = "";
            text_Level.Text = "";
            text_locked.Text = "";
            text_Type.Text = "";
            text_statMain.Text = "";
            text_statSub1.Text = "";
            text_statSub2.Text = "";
            text_statSub3.Text = "";
            text_statSub4.Text = "";
            text_character.Text = "";
        }

        public void DisplayArtifact(Artifact item)
        {
            
            if (item.level != null)
                text_Level.Text = item.level.Value.Text;
            else
                text_Level.Text = "";

            text_locked.Text = item.locked.ToString();

            if (item.piece != null)
                text_Type.Text = item.piece.Value.Text;
            else
                text_Type.Text = "";

            if (item.main != null)
                text_statMain.Text = item.main.Value.Text;
            else
                text_statMain.Text = "";

            text_statSub1.Text = "";
            text_statSub2.Text = "";
            text_statSub3.Text = "";
            text_statSub4.Text = "";
            if (item.subs != null)
            {
                if (item.subs.Count > 0)
                {
                    text_statSub1.Text = item.subs[0].Text;
                }
                if (item.subs.Count > 1)
                {
                    text_statSub2.Text = item.subs[1].Text;
                }
                if (item.subs.Count > 2)
                {
                    text_statSub3.Text = item.subs[2].Text;
                }
                if (item.subs.Count > 3)
                {
                    text_statSub4.Text = item.subs[3].Text;
                }
            }

            if (item.set != null)
                text_Set.Text = item.set.Value.Text;
            else
                text_Set.Text = "";

            if (item.character != null)
                text_character.Text = item.character.Value.Text;
            else
                text_character.Text = "";
        }
    }
}
