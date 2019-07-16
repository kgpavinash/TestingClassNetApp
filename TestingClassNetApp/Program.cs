using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Vision.V1;
using GoogleVisionResultsLibrary;

namespace TestingClassNetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
        }

        static void Test0()
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\aprabhakar\Desktop\snakes\testDAT\DrugVision-a4b475787d6d.json");
            Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            GoogleVisionResults g = new GoogleVisionResults();
            string sourceDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testSource\";
            string destinationParent = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testDestination\";
            string destinationErrorDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testErrorDestination\";
            string destinationEmptyDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testEmptyDestination\";
            string[] files = Directory.GetFiles(sourceDirectory);
            string srcFileName = null, srcFileNameNoExt = null, srcFileExtension = null, srcFilePath = null, srcDirectoryName = null, destinationFile = null, destination = null;
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    srcFilePath = files[i];
                    srcDirectoryName = Path.GetDirectoryName(srcFilePath);
                    srcFileName = Path.GetFileName(files[i]);
                    srcFileNameNoExt = Path.GetFileNameWithoutExtension(files[i]);
                    srcFileExtension = Path.GetExtension(files[i]);
                    destinationFile = destinationParent + srcFileNameNoExt + "\\" + srcFileName;
                    destination = destinationParent + srcFileNameNoExt;
                    Directory.CreateDirectory(destinationParent + srcFileNameNoExt);
                    if (srcFileExtension.Equals(".jpg"))
                    {
                        g.googleVisionResults(srcFilePath, destination);
                        File.Move(srcFilePath, destinationFile);
                    }
                    else
                    {
                        File.Move(srcFilePath, destinationFile);
                    }
                }
                catch (InvalidOperationException e) //Error if Google Credentials are not set properly.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (ImageEmptyOrCorruptException e)      //Error if Source File (an image file) is empty or corrupt.
                {

                    Console.WriteLine(e.Message);
                    Directory.CreateDirectory(destinationErrorDirectory + srcFileNameNoExt);
                    destinationFile = destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileName;
                    File.Move(srcFilePath, destinationFile);
                    File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                    i = i + 1;
                    Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    //Console.ReadKey();

                }
                catch (FileNotFoundException e)    //Error if Source File is not not valid OR if no access to File/Directory
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (DirectoryNotFoundException e)  //Error if Source Directory is not a valid Path.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (UnauthorizedAccessException e)    //Error if there is no permission to access Destination Directory OR if no permission to create Destination Directory.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (AnnotateImageException e)    //Error if Google Cloud Services has problems.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (EmptyResultsException e)  //Error if no text was found by Google Vision.
                {
                    Console.WriteLine(e.Message);
                    Directory.CreateDirectory(destinationEmptyDirectory + srcFileNameNoExt);
                    destinationFile = destinationEmptyDirectory + srcFileNameNoExt + "\\" + srcFileName;
                    File.Move(srcFilePath, destinationFile);
                    File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationEmptyDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                    i = i + 1;
                    Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    //Console.ReadKey();
                }
            }
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        static void Test1()
        {
            GoogleVisionResults g = new GoogleVisionResults();
            string sourceDirectory = @"\\hbm001inddat001\d$\Large Data Folders\Unique Container Label Images For OnBase Completed";
            string destinationParent = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testDestination\";
            string destinationErrorDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testErrorDestination\";
            string destinationEmptyDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testEmptyDestination\";
            string destinationEmptyDetectTextDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testEmptyDetectTextDirectory\";
            string destinationEmptyDetectDocumentTextDirectory = @"C:\Users\aprabhakar\Desktop\snakes\CVStuff\testEmptyDetectDocumentTextDirectory\";
            string[] files = Directory.GetFiles(sourceDirectory,"*.jpg");
            string srcFileName = null, srcFileNameNoExt = null, srcFileExtension = null, srcFilePath = null, srcDirectoryName = null, destinationFile = null, destination = null;
            string logText = null;

            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    srcFilePath = files[i];
                    srcDirectoryName = Path.GetDirectoryName(srcFilePath);
                    srcFileName = Path.GetFileName(files[i]);
                    srcFileNameNoExt = Path.GetFileNameWithoutExtension(files[i]);
                    srcFileExtension = Path.GetExtension(files[i]);
                    destinationFile = destinationParent + srcFileNameNoExt + "\\" + srcFileName;
                    destination = destinationParent + srcFileNameNoExt;
                    Directory.CreateDirectory(destinationParent + srcFileNameNoExt);
                    g.googleVisionResults(srcFilePath, destination);
                    File.Move(srcFilePath, destinationFile);
                    File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationParent + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                }
                catch (InvalidOperationException e) //Error if Google Credentials are not set properly.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (ImageEmptyOrCorruptException e)      //Error if Source File (an image file) is empty or corrupt.
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        Directory.CreateDirectory(destinationErrorDirectory + srcFileNameNoExt);
                        destinationFile = destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileName;
                        File.Move(srcFilePath, destinationFile);
                        File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    }
                    catch //Deals with the case if the image file is already moved to destination. Reverts it back to original directory
                    {
                        if (File.Exists(destinationFile))
                        {
                            File.Move(destinationFile, srcFilePath);
                            i = i + 1;
                        }
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                        Directory.Delete(destinationErrorDirectory + srcFileNameNoExt,true);
                        logText = "In Catch for ImageEmptyOrCorruptException.Reverting image to original destination. " + srcFileNameNoExt;
                        File.WriteAllText(@"WriteText.txt", logText);
                    }

                }
                catch (FileNotFoundException e)    //Error if Source File is not not valid OR if no access to File/Directory
                {
                    Console.WriteLine(e.Message);
                    if (File.Exists(destinationFile))
                    {
                        File.Move(destinationFile, srcFilePath);
                        i = i + 1;
                    }
                    Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    //Directory.Delete(destinationErrorDirectory + srcFileNameNoExt, true);
                    logText = "In Catch for FileNotFoundException.Reverting image to original destination. " + srcFileNameNoExt;
                    File.WriteAllText(@"WriteText.txt", logText);
                    Console.ReadKey();
                    break;
                }
                catch (DirectoryNotFoundException e)  //Error if Source Directory is not a valid Path.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (UnauthorizedAccessException e)    //Error if there is no permission to access Destination Directory OR if no permission to create Destination Directory.
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    break;
                }
                catch (AnnotateImageException e)    //Error if Google Cloud Services has problems.
                {
                    Console.WriteLine(e.Message);
                    Directory.CreateDirectory(destinationErrorDirectory + srcFileNameNoExt);
                    destinationFile = destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileName;
                    File.Move(srcFilePath, destinationFile);
                    File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationErrorDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf"); //What if no okf
                    Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    Console.ReadKey();
                    break;
                }
                catch (EmptyResultsException e)  //Error if no text was found by Google Vision.
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        Directory.CreateDirectory(destinationEmptyDirectory + srcFileNameNoExt);
                        destinationFile = destinationEmptyDirectory + srcFileNameNoExt + "\\" + srcFileName;
                        File.Move(srcFilePath, destinationFile);
                        File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationEmptyDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    }
                    catch
                    {
                        if (File.Exists(destinationFile))
                        {
                            File.Move(destinationFile, srcFilePath);
                            i = i + 1;
                        }
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                        Directory.Delete(destinationEmptyDirectory + srcFileNameNoExt, true);
                        logText = "In Catch for EmptyResultsException.Reverting image to original destination. " + srcFileNameNoExt;
                        File.WriteAllText(@"WriteText.txt", logText);
                    }
                }
                catch (EmptyDetectTextException e)
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        Directory.CreateDirectory(destinationEmptyDetectTextDirectory + srcFileNameNoExt);
                        destinationFile = destinationEmptyDetectTextDirectory + srcFileNameNoExt + "\\" + srcFileName;
                        File.Move(srcFilePath, destinationFile);
                        File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationEmptyDetectTextDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    }
                    catch
                    {
                        if (File.Exists(destinationFile))
                        {
                            File.Move(destinationFile, srcFilePath);
                            i = i + 1;
                        }
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                        Directory.Delete(destinationEmptyDetectTextDirectory + srcFileNameNoExt, true);
                        logText = "In Catch for EmptyDetectTextException.Reverting image to original destination. " + srcFileNameNoExt;
                        File.WriteAllText(@"WriteText.txt", logText);
                    }
                }
                catch (EmptyDetectDocumentTextException e)
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        Directory.CreateDirectory(destinationEmptyDetectDocumentTextDirectory + srcFileNameNoExt);
                        destinationFile = destinationEmptyDetectDocumentTextDirectory + srcFileNameNoExt + "\\" + srcFileName;
                        File.Move(srcFilePath, destinationFile);
                        File.Move(srcDirectoryName + "\\" + srcFileNameNoExt + ".okf", destinationEmptyDetectDocumentTextDirectory + srcFileNameNoExt + "\\" + srcFileNameNoExt + ".okf");
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    }
                    catch
                    {
                        if (File.Exists(destinationFile))
                        {
                            File.Move(destinationFile, srcFilePath);
                            i = i + 1;
                        }
                        Directory.Delete(destinationParent + srcFileNameNoExt, true);
                        Directory.Delete(destinationEmptyDetectDocumentTextDirectory + srcFileNameNoExt, true);
                        logText = "In Catch for EmptyDetectDocumentTextException.Reverting image to original destination. " + srcFileNameNoExt;
                        File.WriteAllText(@"WriteText.txt", logText);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (File.Exists(destinationFile))
                    {
                        File.Move(destinationFile, srcFilePath);
                        i = i + 1;
                    }
                    Directory.Delete(destinationParent + srcFileNameNoExt, true);
                    logText = "In Catch for General Exception.Reverting image to original destination. " + srcFileNameNoExt;
                    File.WriteAllText(@"WriteText.txt", logText);
                }
            }
            Console.WriteLine("Finished");
            Console.ReadKey();
        }


    }
}
