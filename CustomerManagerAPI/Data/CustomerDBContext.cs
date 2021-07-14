using CustomerManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace CustomerManagerAPI.Data
{
    // This class behaves similar to the database class but we are reading data from the yaml file instead of an actual DB
    public class CustomerDBContext
    {
        // Customer data as a list of customers
        public List<Customer> Customers { get; set; }

        public CustomerDBContext()
        {
            Customers = new List<Customer>();
            // Read customers data from yaml
            ReadCustomerDataFromYaml();
        }

        // This method reads the customer node of the yaml file and add cutomer data to the customers list
        public void ReadCustomerDataFromYaml()
        {
            // Get yaml file path
            string yamlPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Data"), "exercise.yml");
            if (File.Exists(yamlPath))
            {
                using (var reader = new StreamReader(yamlPath))
                {
                    // Load the stream
                    YamlStream yamlStream = new YamlStream();
                    yamlStream.Load(reader);
                    YamlMappingNode mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                    foreach (KeyValuePair<YamlNode, YamlNode> childMappingNode in mappingNode.Children)
                    {
                        // Get the customer node
                        if (childMappingNode.Key.ToString() == "customers")
                        {
                            YamlSequenceNode customerNode = (YamlSequenceNode)childMappingNode.Value;
                            foreach (YamlNode childCustomerNode in customerNode.Children)
                            {
                                // Create new customer object for each child customer node
                                Customer customer = new Customer
                                {
                                    Id = childCustomerNode["id"].ToString(),
                                    NumberOfEmployees = long.Parse(childCustomerNode["num_employees"].ToString()),
                                    Name = childCustomerNode["name"].ToString()
                                };
                                // Add tags as string list
                                YamlSequenceNode tagsNode = (YamlSequenceNode)childCustomerNode["tags"];

                                foreach (YamlNode tagNode in tagsNode.Children)
                                {
                                    customer.Tags.Add(tagNode.ToString());
                                }

                                // Add customer model to the customers list
                                Customers.Add(customer);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else
            {
                // Throw file not found exception
                throw new FileNotFoundException($"Failed to find yaml Database file. Expected location is {yamlPath}");
            }
        }
    }
}
