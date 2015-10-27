using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace ShortestPathWithStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get source node
            Console.Write("Enter source: ");
            string src = Console.ReadLine();

            // Get destination node
            Console.Write("\nEnter destination: ");
            string dst = Console.ReadLine();

            // Variable to keep track of visited nodes
            string visitedNodes = string.Empty;

            // Variable to keep track of paths from source to destination
            List<string> paths = new List<string>();
            
            // Add source to visitedNodes variable
            visitedNodes = src.ToUpper();

            // Variable to hold nodes and each node's neighbors
            string nodes = "_ABI_BACD_CBDE_DBCG_ECF_FEI_GDH_HGMN_IAFK_JKL_KIJL_LJKN_MHN_NHLMP_OPQS_PNOS_QORS_RQS_SOPQR_";
            
            // Get the source node's neighbors
            string neighbors = GetNeighbors(src, nodes);
            
            // Call ShortestPath method
            ShortestPath(src.ToUpper(), dst.ToUpper(), nodes, visitedNodes, paths);

            var vNodes = "";
            var vNodesPath = 0;
            // Write the paths
            Console.WriteLine();
            var sNodes = string.Empty;
            foreach (string path in paths)
            {
                Console.Write(path);

                // Write the value of the path (optional)
                Console.Write(" - " + GetPathValue(path) + "\n");
                if (sNodes.Length == 0)
                {
                    sNodes = path;
                }
                else if (path.Length < sNodes.Length)
                {
                    sNodes = path;
                }

                if(paths.First() == path)
                {
                    vNodes = path;
                    vNodesPath = GetPathValue(vNodes);
                }
                else
                {
                    if (vNodesPath > GetPathValue(path))
                    {
                        vNodes = path;
                        vNodesPath = GetPathValue(path);
                    }
                }                
            }

            vNodesPath = GetPathValue(vNodes);

            // Write total path count
            Console.WriteLine("\nShortest Physical Path: {0} ({1})", sNodes, GetPathValue(sNodes));
            Console.WriteLine("Shortest value Path: {0} ({1})", vNodes, vNodesPath);
            Console.WriteLine("\nTotal paths: {0}", paths.Count);


            

            // Pause...so the window doesn't close
            Console.ReadLine();

        }


        public static void ShortestPath(string src, string dst, string nodes, string visitedNodes, List<string> paths)
        {
            // Make sure paths doesn't already contain the visitedNodes and make sure the last character in visitedNodes is the destination
            if (!paths.Contains(visitedNodes) && visitedNodes.Last().ToString() == dst)
            {
                // Add vistedNodes to paths
                paths.Add(visitedNodes);               
            }
            else if (visitedNodes.Contains(dst) && paths.Contains(visitedNodes))
            {
                // else, remove the last node from visitedNodes and update the source node
                src = visitedNodes.Substring(visitedNodes.Length - 1, 1);
                visitedNodes = visitedNodes.Substring(0, visitedNodes.Length - 1);
            }

            // Get the source node's neighbors
            string neighbors = GetNeighbors(src, nodes);
            
            // Go through each of the node's neighbors...
            foreach (char neighbor in neighbors)
            {
                if (!visitedNodes.Contains(neighbor))
                {
                    // Add the neighbor to the visitedNodes variable
                    visitedNodes = visitedNodes + neighbor;

                    // Recursive Call...
                    ShortestPath(neighbor.ToString(), dst, nodes, visitedNodes, paths);

                    visitedNodes = visitedNodes.Substring(0, visitedNodes.Length - 1);
                }
            }
        }


        // Get neighbors of node in nodes
        public static string GetNeighbors(string node, string nodes)
        {
            int srcStart = nodes.IndexOf("_" + node.ToUpper());
            int next = nodes.IndexOf("_", srcStart + 1);
            string neighbors = nodes.Substring(srcStart + 2, next - srcStart - 2);

            return neighbors;
        }


        // Get value of path
        public static int GetPathValue(string path)
        {
            // Variable to hold values between nodes and their neighbors
            string values = "_AB70,I100_BA70,C10,D25_CB10,D15,E5_DB25,C15,G45_EC5,F5_FE5,I35_GD45,H10_HG10,M45,N160_IA100,F35,K30_JK20,L7_KI30,J20,L90_LJ7,K90,N60_MH45,N210_NH160,L60,M210,P12_OP180,Q65,S300_PN12,O180,S10_QO65,R10,S75_RQ10,S60_SO180,P10,Q75,R60_";

            int position = 0;
            int value = 0;
            foreach (char node in path)
            {
                position++;
                int startPos = values.IndexOf("_" + node);
                int endPos = values.IndexOf("_", startPos + 1);
                string nodeValues = values.Substring(startPos, endPos - startPos);
                nodeValues = nodeValues.Substring(2, nodeValues.Length - 2);
                string[] nodesNeighborsValues = nodeValues.Split(',');

                if (node != path.Last())
                {
                    foreach (string val in nodesNeighborsValues)
                    {
                        if (val != "" && val.First() == path[position])
                        {
                            value = value + Int32.Parse(val.Substring(1, val.Length - 1));
                        }
                    }
                }
            }

            // Return the path's weight...
            return value;
        }
    }
}
