using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using IA_Artrites;
using IA_Artrites.Enumerations;

internal class Program
{
    // Troca o valor de sim/nao do txt para true e flase
    static bool ParseBool(string valor) => valor.Trim().ToLower() == "sim";

    // Fazer o usuario escolher valor atributo
    static bool ReadBoolPromptNumero(string prompt)
    {
        Console.WriteLine($"{prompt}:");
        Console.WriteLine("  [1] Sim");
        Console.WriteLine("  [2] Não");

        while (true)
        {
            Console.Write("> Escolha um número: ");
            string input = Console.ReadLine()?.Trim();

            if (input == "1") return true;
            if (input == "2") return false;

            Console.WriteLine(" Opção inválida. Digite 1 para 'Sim' ou 2 para 'Não'.\n");
        }
    }

    // Fazer o usuario escolher valor do atributo
    static T ReadEnumPromptNumero<T>(string prompt) where T : struct, Enum
    {
        var values = Enum.GetValues(typeof(T));
        var names = Enum.GetNames(typeof(T));

        Console.WriteLine($"\n{prompt}:");

        for (int i = 0; i < names.Length; i++)
        {
            // substitui underscore por espaço ao exibir
            Console.WriteLine($"  [{i + 1}] {names[i].Replace("_", " ")}");
        }

        while (true)
        {
            Console.Write("> Escolha um número: ");
            string input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int escolha) &&
                escolha >= 1 && escolha <= names.Length)
            {
                return (T)values.GetValue(escolha - 1);
            }

            Console.WriteLine("Opção inválida. Digite o número de uma das opções acima.\n");
        }
    }

    // Fazer o usuario escolher valor dos pesos
    public static void ConfigurarPesos(Pesos pesos)
    {
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\nPesos dos atributos: ");
            PropertyInfo[] propriedades = typeof(Pesos).GetProperties();

            for (int i = 0; i < propriedades.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {propriedades[i].Name}: {propriedades[i].GetValue(pesos)}");
            }

            Console.Write("\nDeseja alterar algum peso? (s/n): ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "s")
            {
                Console.Write("Digite o número do atributo que deseja alterar: ");
                if (int.TryParse(Console.ReadLine(), out int numero) &&
                    numero >= 1 && numero <= propriedades.Length)
                {
                    var prop = propriedades[numero - 1];

                    Console.Write($"Digite o novo valor para {prop.Name}: ");
                    if (float.TryParse(Console.ReadLine(), out float novoValor))
                    {
                        prop.SetValue(pesos, novoValor);
                        Console.WriteLine($"Peso de {prop.Name} alterado para {novoValor}");
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. Tente novamente.");
                    }
                }
                else
                {
                    Console.WriteLine("Número inválido. Tente novamente.");
                }
            }
            else
            {
                continuar = false;
            }
        }
    }

    // Conversão InflamaçãoLaborial para calculo
    public static float ValorEnumIL(InflamacaoLaborial il)
    {
        return il switch
        {
            InflamacaoLaborial.ausente => 0f,
            InflamacaoLaborial.nao => 0f,
            InflamacaoLaborial.leve => 0.25f,
            InflamacaoLaborial.moderado => 0.5f,
            InflamacaoLaborial.importante => 0.75f,
            InflamacaoLaborial.muito_importante => 1f,
            _ => 0f
        };
    }
    public static float ValorEnumER(EvidenciasRadiologicas il)
    {
        return il switch
        {
            EvidenciasRadiologicas.ausente => 0f,
            EvidenciasRadiologicas.nao => 0f,
            EvidenciasRadiologicas.leve => 0.25f,
            EvidenciasRadiologicas.moderado => 0.5f,
            EvidenciasRadiologicas.importante => 0.75f,
            EvidenciasRadiologicas.muito_importante => 1f,
            _ => 0f
        };
    }
    public static float ValorEnumTC(TomografiaComputadorizada il)
    {
        return il switch
        {
            TomografiaComputadorizada.ausente => 0f,
            TomografiaComputadorizada.nao => 0f,
            TomografiaComputadorizada.leve => 0.25f,
            TomografiaComputadorizada.moderado => 0.5f,
            TomografiaComputadorizada.importante => 0.75f,
            TomografiaComputadorizada.muito_importante => 1f,
            _ => 0f
        };
    }

    public static void Main(string[] args)
    {
        var casos = new List<Caso>();
        string caminhoArquivo = "C:\\Users\\leo_a\\Downloads\\IA Artrites\\IA Artrites\\CasosArtrites.txt";

        if (File.Exists(caminhoArquivo))
        {
            foreach (var linha in File.ReadLines(caminhoArquivo))
            {
                var campos = linha.Split(';');

                float nrValue;
                if (!float.TryParse(campos[16].Trim(), NumberStyles.Float, new CultureInfo("pt-BR"), out nrValue))
                {
                    nrValue = 0;
                }

                // Testando os valores de entrada
                /*
                Console.WriteLine($"Valor: '{campos[0]}'");
                Console.WriteLine($"Valor: '{campos[1]}'");
                Console.WriteLine($"Valor: '{campos[2]}'");
                Console.WriteLine($"Valor: '{campos[3]}'");
                Console.WriteLine($"Valor: '{campos[4]}'");
                Console.WriteLine($"Valor: '{campos[5]}'");
                Console.WriteLine($"Valor: '{campos[6]}'");
                Console.WriteLine($"Valor: '{campos[7]}'");
                Console.WriteLine($"Valor: '{campos[8]}'");
                Console.WriteLine($"Valor: '{campos[9]}'");
                Console.WriteLine($"Valor: '{campos[10]}'");
                Console.WriteLine($"Valor: '{campos[11]}'");
                Console.WriteLine($"Valor: '{campos[12]}'");
                Console.WriteLine($"Valor: '{campos[13]}'");
                Console.WriteLine($"Valor: '{campos[14]}'");
                Console.WriteLine($"Valor: '{campos[15]}'");
                Console.WriteLine($"Valor: '{campos[16]}'");
                Console.WriteLine($"Valor: '{campos[17]}'");
                Console.WriteLine($"Valor: '{campos[18]}'");
                */

                var caso = new Caso
                {
                    Id = int.Parse(campos[0]),
                    DL = ParseBool(campos[1]),
                    RC = ParseBool(campos[2]),
                    DC = ParseBool(campos[3]),
                    Mob = Enum.Parse<Mobilidade>(campos[4], true),
                    DTS = ParseBool(campos[5]),
                    IL = Enum.Parse<InflamacaoLaborial>(campos[6], true),
                    ER = Enum.Parse<EvidenciasRadiologicas>(campos[7], true),
                    TCSE = Enum.Parse<TomografiaComputadorizada>(campos[8], true),
                    ART = ParseBool(campos[9]),
                    RM = ParseBool(campos[10]),
                    BUR = ParseBool(campos[11]),
                    TOF = ParseBool(campos[12]),
                    SIN = ParseBool(campos[13]),
                    ATG = ParseBool(campos[14]),
                    NR = nrValue,
                    HLAB27 = ParseBool(campos[16]),
                    DJ = ParseBool(campos[17]),
                    Diagnostico = Enum.Parse<Diagnostico>(campos[18].Replace(" ", "_"), true)
                };

                casos.Add(caso);
            }
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado.");
        }

        // testar os valores dos casos
        /*
        foreach (var caso in casos)
        {
            Console.WriteLine($"Id: {caso.Id}, nr: {caso.NR}, Diagnóstico: {caso.Diagnostico}");
        }
        //*/

        Console.WriteLine("\n         Diagnostico de Artrite\n");

        // Novo caso manual
        Console.WriteLine("Digite o novo caso\n");
        var novoCaso = new Caso();
        string? input;

        novoCaso.DL = ReadBoolPromptNumero("DL");
        Console.WriteLine();
        novoCaso.RC = ReadBoolPromptNumero("RC");
        Console.WriteLine();
        novoCaso.DC = ReadBoolPromptNumero("DC");
        Console.WriteLine();
        novoCaso.Mob = ReadEnumPromptNumero<Mobilidade>("Mobilidade");
        Console.WriteLine();
        novoCaso.DTS = ReadBoolPromptNumero("DTS");
        Console.WriteLine();
        novoCaso.IL = ReadEnumPromptNumero<InflamacaoLaborial>("Inflamação Laborial");
        Console.WriteLine();
        novoCaso.ER = ReadEnumPromptNumero<EvidenciasRadiologicas>("Evidências Radiológicas");
        Console.WriteLine();
        novoCaso.TCSE = ReadEnumPromptNumero<TomografiaComputadorizada>("Tomografia Computadorizada");
        Console.WriteLine();
        novoCaso.ART = ReadBoolPromptNumero("ART");
        Console.WriteLine();
        novoCaso.RM = ReadBoolPromptNumero("RM");
        Console.WriteLine();
        novoCaso.BUR = ReadBoolPromptNumero("BUR");
        Console.WriteLine();
        novoCaso.TOF = ReadBoolPromptNumero("TOF");
        Console.WriteLine();
        novoCaso.SIN = ReadBoolPromptNumero("SIN");
        Console.WriteLine();
        novoCaso.ATG = ReadBoolPromptNumero("ATG");
        Console.WriteLine();
        Console.Write("NR (ex: 0,5): ");
        input = Console.ReadLine();
        float nrManual;
        if (!float.TryParse((input ?? "0").Trim(), NumberStyles.Float, new CultureInfo("pt-BR"), out nrManual))
        {
            nrManual = 0;
        }
        novoCaso.NR = nrManual;
        Console.WriteLine();
        novoCaso.HLAB27 = ReadBoolPromptNumero("HLAB27");
        Console.WriteLine();
        novoCaso.DJ = ReadBoolPromptNumero("DJ");
        Console.WriteLine();

        // Pesos dos Casos
        var pesos = new Pesos();
        ConfigurarPesos(pesos);

        // Algoritmo da Vizinhança
        float atributo = 0;
        float somaAtributos = 0;
        int valor1 = 0;
        int valor2 = 0;
        float resultado = 0;
        float somaPesos = pesos.DTS + pesos.DJ + pesos.DC + pesos.ER + pesos.IL +
                              pesos.Mob + pesos.RC + pesos.RM + pesos.NR + pesos.TCSE +
                              pesos.ART + pesos.BUR + pesos.TOF + pesos.SIN + pesos.ATG +
                              pesos.HLAB27 + pesos.DL;

        for (int i = 0; i < casos.Count; i++) {
            atributo = 0;
            somaAtributos = 0;
            valor1 = 0;
            valor2 = 0;
            resultado = 0;

            Console.WriteLine($"Caso {i}: Id = {casos[i].Id}");

            valor1 = Convert.ToInt32(novoCaso.DL);
            valor2 = Convert.ToInt32(casos[i].DL);
            atributo = pesos.DL * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.RC);
            valor2 = Convert.ToInt32(casos[i].RC);
            atributo = pesos.RC * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.DC);
            valor2 = Convert.ToInt32(casos[i].DC);
            atributo = pesos.DC * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.Mob);
            valor2 = Convert.ToInt32(casos[i].Mob);
            atributo = pesos.Mob * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.DTS);
            valor2 = Convert.ToInt32(casos[i].DTS);
            atributo = pesos.DTS * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            atributo = pesos.IL * ((1-(ValorEnumIL(novoCaso.IL) - ValorEnumIL(casos[i].IL))) / (1 - 0));
            somaAtributos += atributo;

            atributo = pesos.ER * ((1-(ValorEnumER(novoCaso.ER) - ValorEnumER(casos[i].ER))) / (1 - 0));
            somaAtributos += atributo;

            atributo = pesos.TCSE * ((1-(ValorEnumTC(novoCaso.TCSE) - ValorEnumTC(casos[i].TCSE))) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.ART);
            valor2 = Convert.ToInt32(casos[i].ART);
            atributo = pesos.ART * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.RM);
            valor2 = Convert.ToInt32(casos[i].RM);
            atributo = pesos.RM * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.BUR);
            valor2 = Convert.ToInt32(casos[i].BUR);
            atributo = pesos.BUR * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.TOF);
            valor2 = Convert.ToInt32(casos[i].TOF);
            atributo = pesos.TOF * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.SIN);
            valor2 = Convert.ToInt32(casos[i].SIN);
            atributo = pesos.SIN * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.ATG);
            valor2 = Convert.ToInt32(casos[i].ATG);
            atributo = pesos.ATG * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            atributo = pesos.NR * ((1 - (novoCaso.NR - casos[i].NR)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.HLAB27);
            valor2 = Convert.ToInt32(casos[i].HLAB27);
            atributo = pesos.HLAB27 * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            valor1 = Convert.ToInt32(novoCaso.DJ);
            valor2 = Convert.ToInt32(casos[i].DJ);
            atributo = pesos.DJ * ((1 - (valor1 - valor2)) / (1 - 0));
            somaAtributos += atributo;

            resultado = (somaAtributos / somaPesos) * 100;
            casos[i].Similaridade = resultado;
        }


        // Mostrar Resultado
        Console.WriteLine("\n\n   Resultado do Diagnóstico\n");
        Console.WriteLine("Pesos dos Casos:");
        PropertyInfo[] propriedades = typeof(IA_Artrites.Pesos).GetProperties();
        string linhaPesos = string.Join(" | ", Array.ConvertAll(propriedades, p => $"{p.Name}:{p.GetValue(pesos)}"));
        Console.WriteLine(linhaPesos);

        Console.WriteLine("\nNovo Caso:");
        PropertyInfo[] propriedadesCaso = typeof(Caso).GetProperties();
        string linhaCaso = string.Join(" | ", Array.ConvertAll(propriedadesCaso, p => $"{p.Name}:{p.GetValue(novoCaso)}"));
        Console.WriteLine(linhaCaso);

        Console.WriteLine("\n\nCasos similares:");
        
        casos = casos.OrderBy(c => c.Similaridade).ToList();

        // Adicionando novo Caso
        Console.WriteLine("\nVai querer adicionar esses novo caso no banco de dados?");
        casos.Add(novoCaso);

        Console.WriteLine("Novo caso adicionado com sucesso!");



    }
}