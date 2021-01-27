'''
Created on 21 nov 2016

@author: Shap
'''

import fbxUtils
import fbx
import sys
        
    
    
#there are addCubes commented. If you want to see the vertices make it not a comment.
#the file is way lighter without them though
def addTrianglesForPoints():
    for line in f:
        a = line.split(", ")
        
        #1st point
        x=float(a[0])
        y=float(a[1])
        z=float(a[2])    
        fbxUtils.addCube(scene, x, y, z, 0.02)
        
        
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
    
	txtExtension = '.txt'
	fbxExtension = '.fbx'

    #open text file and print some info (this will come back to the C# code)
	f=open(filename+ txtExtension,'r')
	print f
    
    #create fbx manager and scene, needed for using the sdk 
	manager = fbx.FbxManager.Create()
	scene = fbx.FbxScene.Create(manager, "")    
    
    #creates a triangle for each set of three points in the file
	addTrianglesForPoints()
    
    #export our file into filename.fbx, ASCII format
	fbxUtils.saveScene(filename + fbxExtension, manager, scene, pAsASCII=True )
    
    #cleanup
	manager.Destroy()
	del manager, scene
    
	print 'done'