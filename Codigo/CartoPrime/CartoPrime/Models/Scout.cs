using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models
{
    public class Scout
    {
        public int A
        {
            get;
            set;
        }

        public int CA
        {
            get;
            set;
        }

        public int CV
        {
            get;
            set;
        }

        public int DD
        {
            get;
            set;
        }

        public int DP
        {
            get;
            set;
        }

        public int FC
        {
            get;
            set;
        }

        public int FD
        {
            get;
            set;
        }

        public int FF
        {
            get;
            set;
        }

        public int FS
        {
            get;
            set;
        }

        public int FT
        {
            get;
            set;
        }

        public int G
        {
            get;
            set;
        }

        public int GC
        {
            get;
            set;
        }

        public int GS
        {
            get;
            set;
        }

        public int I
        {
            get;
            set;
        }

        public int PE
        {
            get;
            set;
        }

        public int PP
        {
            get;
            set;
        }

        public int RB
        {
            get;
            set;
        }

        public int SG
        {
            get;
            set;
        }

        public int PI
        {
            get;
            set;
        }

        public int DS
        {
            get;
            set;
        }

        public int DE { get; set; }

        public Scout()
        {

        }

        public static string OrganizarScouts(Scout scout)
        {
            string scout_tela = "";
            #region scout Ataque
            if (scout != null)
            {
                if (scout.G > 0)
                {
                    scout_tela += scout.G + "G ";
                }
                if (scout.A > 0)
                {
                    scout_tela += scout.A + "A ";
                }
                if (scout.FT > 0)
                {
                    scout_tela += scout.FT + "FT ";
                }
                if (scout.FD > 0)
                {
                    scout_tela += scout.FD + "FD ";
                }
                if (scout.FF > 0)
                {
                    scout_tela += scout.FF + "FF ";
                }
                if (scout.FS > 0)
                {
                    scout_tela += scout.FS + "FS ";
                }
                if (scout.RB > 0)
                {
                    scout_tela += scout.RB + "RB ";
                }
                if (scout.SG > 0)
                {
                    scout_tela += "SG ";
                }
                if (scout.DD > 0)
                {
                    scout_tela += scout.DD + "DD ";
                }
                if (scout.DP > 0)
                {
                    scout_tela += scout.DP + "DP ";
                }
                if (scout.PI > 0)
                {
                    scout_tela += scout.PI + "PI ";
                }
                if (scout.DE > 0)
                {
                    scout_tela += scout.DE + "DE";
                }
            }
            #endregion

            return scout_tela;
        }
        public static string OrganizarScoutsDef(Scout scout)
        {
            string scout_tela = "";
            #region scout Defesa
            if (scout != null)
            {
                if (scout.PE > 0)
                {
                    scout_tela += scout.PE + "PE ";
                }
                if (scout.I > 0)
                {
                    scout_tela += scout.I + "I ";
                }
                if (scout.PP > 0)
                {
                    scout_tela += scout.PP + "PP ";
                }

                if (scout.FC > 0)
                {
                    scout_tela += scout.FC + "FC ";
                }
                if (scout.GC > 0)
                {
                    scout_tela += scout.GC + "GC ";
                }
                if (scout.CA > 0)
                {
                    scout_tela += scout.CA + "CA ";
                }
                if (scout.CV > 0)
                {
                    scout_tela += scout.CV + "CV ";
                }

                if (scout.GS > 0)
                {
                    scout_tela += scout.GS + "GS";
                }
                if (scout.DS > 0)
                {
                    scout_tela += scout.DS + "DS";
                }
            }
            #endregion

            return scout_tela;
        }
    }
}
