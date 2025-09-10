using System;
using GTA;
using GTA.UI;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class KillCounter : Script
{
    private List<int> hashes;
    private TextElement text;
    private int kills;

    public KillCounter()
    {
        text = new TextElement("Kills: 0", new Point(0, 0), .3f);
        text.Enabled = true;
        text.Color = Color.Red;
        text.Draw();

        kills = 0;

        hashes = new List<int>();

        Tick += OnTick;
        KeyUp += OnKeyUp; 
    }

    private void OnTick(object sender, EventArgs e)
    {
        Ped player = Game.Player.Character;
        Ped[] peds = World.GetAllPeds();

        foreach (Ped p in peds)
        {
            if (p.IsDead)
            {
                var hash = p.GetHashCode();

                if (p.Killer != null && p.Killer.Equals(player) && !hashes.Contains(hash))
                {
                    kills++;
                    hashes.Add(hash);
                }
            }
        }

        text.Caption = "Kills: " + kills.ToString();
        text.Draw();

        if (player.IsDead)
        {
            kills = 0;
        }
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.L)
        {
            kills = 0;
        }
    }
}
