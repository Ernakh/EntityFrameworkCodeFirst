using EFCExample.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EFCExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("---Linq Básico---");
            //int[] pontuacao = new int[] { 97, 92, 81, 60 };

            //IEnumerable<int> consultaPontos =
            //    from ponto in pontuacao
            //    where ponto > 80
            //    select ponto;

            //foreach (int i in consultaPontos)
            //{
            //    Console.Write(i + " ");
            //}

            //------------------------------------------------------

            /*
             Database-First Approach
             Code-First Approach
             Model-First Approach
             */

            Console.WriteLine("Informe " +
                "1 para criar uma pessoa, " +
                "2 para alterar nome, " +
                "3 para inserir email, " +
                "4 para excluir a pessoa, " +
                "5 para consultar todos, " +
                "6 para consultar por ID");

            int op = int.Parse(Console.ReadLine());

            Contexto contexto = new Contexto();

            switch (op)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Insira o nome:");
                        Pessoa p = new Pessoa();
                        p.nome = Console.ReadLine();
                        Console.WriteLine("Insira o email:");
                        string temp = Console.ReadLine();
                        p.Emails = new List<Email>()
                        {
                            new Email()
                            {
                                email = temp
                            }
                        };

                        contexto.Pessoas.Add(p);
                        contexto.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        Console.WriteLine("Informe o id da pessoa");
                        int id = int.Parse(Console.ReadLine());
                        Pessoa pAlt = contexto.Pessoas.Find(id);
                        Console.WriteLine("Insira o nome:");
                        pAlt.nome = Console.ReadLine();
                        contexto.Pessoas.Update(pAlt);
                        contexto.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("Informe o id da pessoa");
                    int idEm = int.Parse(Console.ReadLine());
                    Pessoa pEm = contexto.Pessoas.Find(idEm);
                    Console.WriteLine("Insira o novo email:");
                    string emailTemp = Console.ReadLine();
                    pEm.Emails = new List<Email>()
                    {
                        new Email()
                        {
                            email = emailTemp
                        }
                    };

                    contexto.Pessoas.Update(pEm);
                    contexto.SaveChanges();

                    break;
                case 4:
                    Console.WriteLine("Informe o id da pessoa");
                    int idExc = int.Parse(Console.ReadLine());
                    Pessoa pExc = contexto.Pessoas.Find(idExc);

                    Console.WriteLine("Confirma a exclusão de " + pExc.nome);
                    Console.WriteLine("E dos emails: ");

                    foreach (Email itemEmail in pExc.Emails)
                    {
                        Console.WriteLine("   " + itemEmail.email);
                    }

                    Console.WriteLine("1 - Sim, 2 - Não ");

                    if(int.Parse(Console.ReadLine()) == 1)
                    {
                        contexto.Pessoas.Remove(pExc);
                        contexto.SaveChanges();
                    }
                    else if (int.Parse(Console.ReadLine()) == 2)
                    {
                        return;
                    }

                        break;
                case 5:
                    List<Pessoa> lista = new List<Pessoa>();
                    lista = getAllPessoas(contexto);
                    foreach (Pessoa pessoaItem in lista)
                    {
                        Console.WriteLine(pessoaItem.nome);
                        if (pessoaItem.Emails != null)
                        {
                            foreach (Email emailItem in pessoaItem.Emails)
                            {
                                Console.WriteLine("    " + emailItem.email);
                            }

                            Console.WriteLine();
                        }
                    }

                    break;
                case 6:
                    Console.WriteLine("Informe o id da pessoa");
                    int idPessoa = int.Parse(Console.ReadLine());
                    Pessoa pessoa = getPessoa(idPessoa, contexto);
                    Console.WriteLine(pessoa.nome);

                    if (pessoa.Emails != null)
                    {
                        foreach (Email item in pessoa.Emails)
                        {
                            Console.WriteLine("    " + item.email);
                        }
                    }

                    break;
                default:
                    break;
            }

            static Pessoa getPessoa(int id, Contexto contexto)
            {
                //return
                //     (from Pessoa p in contexto.Pessoas
                //      where p.id == id
                //      select p).Include(e => e.Emails).ToList<Pessoa>()[0];

                //return contexto.Pessoas.Find(id);

                //.Pessoas
                //.Where(p => p.id == id).SingleOrDefault<Pessoa>();

                //exemplo que traz só a pessoa
                //return contexto
                //.Pessoas
                //.Where(p => p.id == id).SingleOrDefault<Pessoa>();


                //List<Pessoa> teste = contexto.Pessoas
                //    .Include(p => p.Emails)
                //    .ToList<Pessoa>();

                return contexto.Pessoas
                    .Include(p => p.Emails)
                    .FirstOrDefault(x => x.id == id);

                //var x =
                //    (from a in contexto.Pessoas.Where(e => e.id == id)
                //     join b in contexto.Emails on a.id equals b.pessoa.id).
                //     Include(e => contexto.Emails)
                //     .Select<Pessoa>();

                //return x;

                //return
                //    (from p in contexto.Pessoas.Include("Emails")
                //     where p.id == id
                //     select p).SingleOrDefault<Pessoa>();

                //return
                //    (from p in contexto.Pessoas.Include(e => e.Emails.Where(x => x.pessoa.id == id))
                //     where p.id == id
                //     select p).SingleOrDefault<Pessoa>();

                //return null;
            }
        }

        private static List<Pessoa> getAllPessoas(Contexto contexto)
        {
            return
            (from Pessoa p in contexto.Pessoas
             select p).Include(e => e.Emails).ToList<Pessoa>();
        }
    }
}
