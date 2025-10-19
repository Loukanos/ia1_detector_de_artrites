using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_Artrites;
public class Pesos
{
    public float DL { get; set; } = 0.70f; // Dor Lombar
    public float RC { get; set; } = 0.60f; // Rigidez na Coluna
    public float DC { get; set; } = 0.30f;// Deformação na Coluna
    public float Mob { get; set; } = 0.50f;// Mobilidade
    public float DTS { get; set; } = 0.10f;// Dor ao Toque no Sacroiliaco
    public float IL { get; set; } = 0.90f;// Inflamação Laboratorial
    public float ER { get; set; } = 0.90f;// Evidencias Radiológicas
    public float TCSE { get; set; } = 0.20f;// Tomografia Computadorizada
    public float ART { get; set; } = 0.80f;// Artrite
    public float RM { get; set; } = 0.30f;// Rigidez Matinal
    public float BUR { get; set; } = 0.05f;// Bursite
    public float TOF { get; set; } = 0.05f;// Tophi
    public float SIN { get; set; } = 0.10f;// Sinovite
    public float ATG { get; set; } = 0.05f;// Artralgia
    public float NR { get; set; } = 0.05f;// Nódulos Reumatoides
    public float HLAB27 { get; set; } = 0.15f;// HLA-B27
    public float DJ { get; set; } = 0.35f; // Deformação nas Juntas

}
