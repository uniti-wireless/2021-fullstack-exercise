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
    public class UserDBContext
    {
        // User login data as a list of users
        public List<LoginModel> Users { get; set; }

        public UserDBContext()
        {
            Users = new List<LoginModel>();
            // Read user authentication credentials from yaml
            ReadUserCredentials();
        }

        // This method reads the auth node of the yaml file and add user's creadentils to the users list as login model objects
        public void ReadUserCredentials()
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
                    // the rest
                    YamlMappingNode mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                    foreach (KeyValuePair<YamlNode, YamlNode> childNode in mappingNode.Children)
                    {
                        // Read auth node
                        if (childNode.Key.ToString() == "auth")
                        {
                            YamlSequenceNode userNode = (YamlSequenceNode)childNode.Value;
                            foreach (YamlNode childUserNode in userNode.Children)
                            {
                                // Create user as Login model object
                                LoginModel user = new LoginModel
                                {
                                    UserName = childUserNode["username"].ToString(),
                                    Password = childUserNode["password"].ToString(),
                                };
                                // add user to the users list
                                Users.Add(user);
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
