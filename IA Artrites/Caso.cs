using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA_Artrites.Enumerations;

namespace IA_Artrites;
public class Caso
{
    public int Id { get; set; }
    public bool DL { get; set; } // Dor Lombar
    public bool RC { get; set; } // Rigidez na Coluna
    public bool DC { get; set; } // Deformação na Coluna
    public Mobilidade Mob { get; set; } // Mobilidade
    public bool DTS { get; set; } // Dor ao Toque no Sacroiliaco
    public InflamacaoLaborial IL { get; set; } // Inflamação Laboratorial
    public EvidenciasRadiologicas ER { get; set; } // Evidencias Radiológicas
    public TomografiaComputadorizada TCSE { get; set; } // Tomografia Computadorizada
    public bool ART { get; set; } // Artrite
    public bool RM { get; set; } // Rigidez Matinal
    public bool BUR { get; set; } // Bursite
    public bool TOF { get; set; } // Tophi
    public bool SIN { get; set; } // Sinovite
    public bool ATG { get; set; } // Artralgia
    public float NR { get; set; } // Nódulos Reumatoides
    public bool HLAB27 { get; set; } // HLA-B27
    public bool DJ { get; set; } // Deformação nas Juntas
    public Diagnostico Diagnostico { get; set; } // Diagnostico
    public float Similaridade { get; set; } // Grau de Similaridade
}
