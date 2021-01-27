'''
Created on Nov 29, 2016

@author: Shapour
'''

import fbx
import FbxCommon
from fbx import FbxIOSettings, IOSROOT
import sys

def saveScene( pFilename, pFbxManager, pFbxScene, pAsASCII=False ):
    ''' Save the scene using the Python FBX API '''
    exporter = fbx.FbxExporter.Create( pFbxManager, '' )
    
    if pAsASCII:
        #DEBUG: Initialize the FbxExporter object to export in ASCII.
        asciiFormatIndex = getASCIIFormatIndex( pFbxManager )
        isInitialized = exporter.Initialize( pFilename, asciiFormatIndex )
    else:
        isInitialized = exporter.Initialize( pFilename )
    
    if( isInitialized == False ):
        raise Exception( 'Exporter failed to initialize. Error returned: ' + str( exporter.GetStatus().GetErrorString() ) )
    
    exporter.Export( pFbxScene )
    
    exporter.Destroy()

def getASCIIFormatIndex( pManager ):
    ''' Obtain the index of the ASCII export format. '''
    # Count the number of formats we can write to.
    numFormats = pManager.GetIOPluginRegistry().GetWriterFormatCount()
    
    # Set the default format to the native binary format.
    formatIndex = pManager.GetIOPluginRegistry().GetNativeWriterFormat()
    
    # Get the FBX format index whose corresponding description contains "ascii".
    for i in range( 0, numFormats ):
        
        # First check if the writer is an FBX writer.
        if pManager.GetIOPluginRegistry().WriterIsFBX( i ):
            
            # Obtain the description of the FBX writer.
            description = pManager.GetIOPluginRegistry().GetWriterFormatDescription( i )
            
            # Check if the description contains 'ascii'.
            if 'ascii' in description:
                formatIndex = i
                break
    
    # Return the file format.
    return formatIndex

def getFilename(s):
    #the args should be something like: ['main.py','filename.txt']
    
    #get the second argument
    tmp = s.split(',')[1]
    #tmp is something like " 'filename.extension']", we get rid of the space and the single quote
    tmp = tmp[2:]
    #we get rid of the ']
    tmp = tmp[:-2]
    return tmp

if __name__ == '__main__':
	
    #get filename from command line
	filename = getFilename(str(sys.argv))
	
	manager = fbx.FbxManager.Create()
	ios = FbxIOSettings.Create(manager,IOSROOT)
    
	manager.SetIOSettings(ios)        
         
	importer = fbx.FbxImporter.Create(manager,'')
	format = ".fbx"
    
	importStatus = importer.Initialize(filename + format, -1, manager.GetIOSettings())
    
	print importStatus
    
    
	scene = fbx.FbxScene.Create(manager, "")    
    
	importer.Import(scene)
    
	saveScene(filename + "ASCII" + format, manager, scene, pAsASCII=True) 
    
	print "Hello"