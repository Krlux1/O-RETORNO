using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace UsandoBanco
{
    internal class Menu
    {
        private int _CurrentID = 1;
        private List<Produto> _Produtos = new List<Produto>();
        public void ChamarMenu()
        {
            Console.WriteLine($"============================");
            Console.WriteLine($"     Sistema de vendas");
            Console.WriteLine($"============================");

            Console.WriteLine($"1 - Listar (Read/Select) ");
            Console.WriteLine($"2 - Adicionar (Create/Isert)");
            Console.WriteLine($"3 - Deletar (Delete)");
            Console.WriteLine($"4 - Atualizar (Update)");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();

                    var produtos = Produto.GetAll();

                    ListarItens(produtos);
                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "2":
                    Produto produto = new Produto();
                    produto.Save();


                    Console.WriteLine("Produto adicionado!!");

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "3":
                    Console.Clear();

                    Console.WriteLine("Informe o ID que deseja deletar:");
                    var  idDeletar = long.Parse(Console.ReadLine());
                    var produtoDeletar = new Produto(idDeletar);

                    if (produtoDeletar.isValid())
                    {
                        produtoDeletar.Delete();
                        Console.WriteLine("Produto excluído!");
                    }
                    else
                    {
                        Console.WriteLine($"Não existe o produto com o ID {idDeletar}");
                    }

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;

                case "4":
                    Console.WriteLine($"Informe o ID do produto: ");
                    var idUpdate = long.Parse(Console.ReadLine());
                    var produtoUpdate = new Produto(idUpdate);

                    if (produtoUpdate.isValid())
                    {
                        AlterarItem(produtoUpdate);
                    }
                    else
                    {
                        Console.WriteLine($"Não existe o produto com o ID {idUpdate}");
                    }
                 
                    Console.Clear();
                    ChamarMenu();
                    break;

                default:
                    Console.WriteLine("Opção invalida");

                    Console.ReadKey();
                    Console.Clear();
                    ChamarMenu();
                    break;
            }
        }

        private void AlterarItem(Produto produto)
        {
            Console.Clear();
            Console.WriteLine($"Menu de alteração");
            Console.WriteLine($"1 - Nome");
            Console.WriteLine($"2 - Código");
            Console.WriteLine($"3 - Valor");
            Console.WriteLine($"4 - Descrição");
            Console.WriteLine($"'Salva' para salvar as alterações");

            Console.WriteLine("Informe o campo que deseja alterar: ");
          var entrada = Console.ReadLine().ToUpper();
            while ( entrada != "SALVAR")
            {
                if (entrada == "1" && entrada != "2" && entrada != "3" && entrada != "4")
                {
                    Console.WriteLine("Campo inválido!");
                }
                else
                {



                    Console.Write("Informe o valor que será aplicado: ");
                    var valor = Console.ReadLine();
                    switch (entrada)
                    {
                        case "1":
                            produto.Nome = entrada;
                            break;
                        case "2":
                            produto.Codigo = valor;
                            break;
                        case "3":
                            produto.Valor = decimal.Parse(valor);
                            break;
                        case "4":
                            produto.Descricao = valor;
                            break;

                    }
                    
                }
                Console.Clear();
                Console.WriteLine($"Menu de alteração");
                Console.WriteLine($"1 - Nome");
                Console.WriteLine($"2 - Código");
                Console.WriteLine($"3 - Valor");
                Console.WriteLine($"4 - Descrição");
                Console.WriteLine($"'Salva' para salvar as alterações");

                Console.WriteLine("\nInforme o campo que deseja alterar: ");
                entrada = Console.ReadLine().ToUpper();
            }

            produto.Update();
          
        }



        private void AdicionarItem(Produto produto)
        {
            _Produtos.Add(produto);

        }

        private void ListarItens(List <Produto> itens)
        {
            Console.Clear ();

            if (itens.Count < 1)
            {
                Console.WriteLine("Sem itens para ixibir!");
            }
            foreach (Produto produto in itens)
            {
                Console.WriteLine($"Produto: {produto.Id}" +
                    $"\n\t Nome: {produto.Nome}" +
                    $"\n\t Codigo: {produto.Codigo}" +
                    $"\n \t Valor: {produto.Valor:f2}" +
                    $"\n \t Descrição: {produto.Descricao}");


            }
        }

        private void DeletarItem(int id)
        {
            _Produtos.RemoveAll(produto => produto.Id == id);

            //foreach(Produto produto in _Produtos)
            //{
            //    if(produto.Id == id)
            //    {
            //        _Produtos.Remove(produto);
            //    }
            //}
        }





    }
}
