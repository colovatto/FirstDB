using Microsoft.EntityFrameworkCore;

namespace FirstDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite:\n" +
               "1 para criar uma pessoa\n" +
               "2 para alterar o nome da pessoa\n" +
               "3 para inserir email\n" +
               "4 para excluir uma pessoa \n" +
               "5 para consultar TUDO\n" +
               "6 para consultar pelo ID"
               );

            int op = int.Parse(Console.ReadLine());

            DbfirstContext contexto = new DbfirstContext();

            switch (op)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Insera o nome da pessoa:");
                        Pessoa p = new Pessoa();
                        p.Nome = Console.ReadLine();

                        //modo 1
                        Console.WriteLine("Insira um email:");
                        string emailTemp = Console.ReadLine();

                        p.Emails = new List<Email>()
                        {
                            new Email()
                            {
                                Email1 = emailTemp
                            }
                        };

                        //modo 2
                        //Email e = new Email();
                        //Console.WriteLine("Insira um email:");
                        //e.email = Console.ReadLine();

                        //p.emails = new List<Email>();
                        //p.emails.Add(e);

                        contexto.Pessoas.Add(p);
                        contexto.SaveChanges();

                        Console.WriteLine("Pessoa inserida com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("Informe Id da Pessoa: ");
                        int idPessoa = int.Parse(Console.ReadLine());

                        Pessoa? pAlt = contexto.Pessoas.Find(idPessoa);

                        Console.WriteLine("Informe o nome correto: ");
                        pAlt.Nome = Console.ReadLine();

                        contexto.Pessoas.Update(pAlt);
                        contexto.SaveChanges();

                        Console.WriteLine("Nome alterado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 3:
                    try
                    {
                        Console.WriteLine("Informe o ID da pessoa: ");
                        int id = int.Parse(Console.ReadLine());

                        Pessoa? p = contexto.Pessoas.Find(id);

                        Console.WriteLine("Informe o novo email: ");
                        string emailTemp = Console.ReadLine();

                        p.Emails.Add(new Email()
                        {
                            Email1 = emailTemp
                        });

                        contexto.Pessoas.Update(p);
                        contexto.SaveChanges();

                        Console.WriteLine("Email inserido com sucesso!");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 4:
                    try
                    {
                        Console.WriteLine("Informe o ID para exclusão: ");
                        int id = int.Parse(Console.ReadLine());
                        Pessoa p = contexto.Pessoas.Find(id);

                        Console.WriteLine("Confirmar a exclusão de " + p.Nome);
                        Console.WriteLine("E dos seus emails?");

                        foreach (Email item in p.Emails)
                        {
                            Console.WriteLine("\t" + item.Email1);
                        }

                        Console.WriteLine("1 para SIM e 2 para NÃO");

                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            contexto.Pessoas.Remove(p);
                            contexto.SaveChanges();

                            Console.WriteLine("Excluído com sucesso!");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 5:
                    List<Pessoa> pessoas = (from Pessoa p in contexto.Pessoas select p)
                        .Include(pes => pes.Emails).ToList<Pessoa>();

                    foreach (Pessoa item in pessoas)
                    {
                        Console.WriteLine(item.Id + " - " + item.Nome);

                        foreach (Email itemE in item.Emails)
                        {
                            Console.WriteLine("\t" + itemE.Email1);
                        }
                    }

                    break;

                case 6:
                    try
                    {
                        Console.WriteLine("Informe o ID da pessoa: ");
                        int idP = int.Parse(Console.ReadLine());

                        Pessoa pessoa = contexto.Pessoas.Include(p => p.Emails).FirstOrDefault(x => x.Id == idP);

                        Console.WriteLine(pessoa.Id + " - " + pessoa.Nome);

                        foreach (Email item in pessoa.Emails)
                        {
                            Console.WriteLine("\t" + item.Email1);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}