'''
Created on 21 nov 2016

@author: Shap
'''

import fbxUtils
import fbx
import sys
        
def drawTriangle(v,v2,v3,trMesh, antiClockwise):        
    trMesh.BeginPolygon()    
    i = 0       
    
    if (antiClockwise):
        tmp = fbx.FbxVector4(v3)
        v3 = fbx.FbxVector4(v2)
        v2 = fbx.FbxVector4(tmp)
    
    trMesh.SetControlPointAt(v, i)
    trMesh.AddPolygon(i)
    i = i + 1
    trMesh.SetControlPointAt(v2, i)
    trMesh.AddPolygon(i)
    i = i + 1
    trMesh.SetControlPointAt(v3, i)
    trMesh.AddPolygon(i)
    i = i + 1
    
    trMesh.EndPolygon()
    
    
#there are addCubes commented. If you want to see the vertices make it not a comment.
#the file is way lighter without them though
def addTrianglesForPoints():
    for line in f:
        a = line.split()
        
        #1st point
        x=float(a[0])
        y=float(a[1])
        z=float(a[2])    
        fbxUtils.addCube(scene, x, y, z, 3)
        
        #2nd point
        x2=float(a[3])
        y2=float(a[4])
        z2=float(a[5])    
        fbxUtils.addCube(scene, x2, y2, z2, 3)
        
        #3rd point
        x3=float(a[6])
        y3=float(a[7])
        z3=float(a[8])            
        fbxUtils.addCube(scene, x3, y3, z3, 3)
        
        #init node, mesh and vertices
        trNode = fbx.FbxNode.Create(manager, "")
        trMesh = fbx.FbxMesh.Create(scene, '')        
        v = fbx.FbxVector4(x, y, z)
        v2 = fbx.FbxVector4(x2, y2, z2)
        v3 = fbx.FbxVector4(x3, y3, z3)
        
        #draw the triangle meshes for both faces
        
        drawTriangle(v,v2,v3,trMesh, 1!=1)
        trNode.AddNodeAttribute(trMesh )

        root_node = scene.GetRootNode()
        root_node.AddChild(trNode)    
        
        trNode = fbx.FbxNode.Create(manager, "")
        trMesh = fbx.FbxMesh.Create(scene, '')          
        drawTriangle(v,v2,v3,trMesh, 1==1)
        trNode.AddNodeAttribute(trMesh )
        
        root_node = scene.GetRootNode()
        root_node.AddChild(trNode)    
        
        
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