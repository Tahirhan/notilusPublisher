# notilusPublisher
Publisher for Rhino Plugins - Sends bins and creates Rhi files automatically to server or wherever you want

## How it Works

Program gets an abbreviation for the plugin name from the user, Rhino version, and Database type. These are the details I need for the plugin publishments but details can be removed or increased.

After inputs program creates a folder with the given details and date in the specified publishment path. If there is a folder with the same details and date, program deletes it and creates a new one.

After folder creation, the program copies bin folder of the plugin and creates an installer RHI file with the plugin name. The program gets plugin name automatically from the entered path.

## Integrate to Your Workspace

First change local plugin project folder paths and publish paths in the Program.cs -> pathPairs dictionary.

I use abbreviations instead of writing the whole plugin name, so you should update the abbreviations dictionary in csServerPublisher.cs file.
