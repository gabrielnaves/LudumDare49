# What this script does:
#  Converts fonts in spritesheet format to a Unity CustomFont, calculating each character's index, uvs, verts and other data.
#  The spritesheet MUST have all characters inside the same 'box'. For example, if char 'A' is the largest character, and has width of 100 and char 'I' has width of 20 pixels, char 'I' must have empty space around it in order to occupy 100 pixels.
#
# Inputs:
#  - char sequence
#  - target sprite
#  - character spacing
#  - line spacing
#  - spacebar width
# Output:
#  - a text file in Unity's CustomFont format.
#
# Algorithm (brief):
#  1) Add header
#  2) Write characters
#    2.1) calculate char ascii index, uvs, verts and advance as if each character actually occupied 100% of their bounding box
#    2.1) reads pixel data for each column, both to the left and to the right, to adjust uvs, verts and advance so as to remove empty columns
#  3) Add footer

import cv2
import numpy

# chars = "<ABCDEFGHIJKLMNOPQRSTUVWXYZ"
# sprite = "actionButtonFont.png"
chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ().,\"'!?+-0123456789:"
sprite = "mainFont.png"
characterSpacing = 1
lineSpacing = 2
spacebarWidth = 2
fontScaling = 1 ## change for larger fonts, only use integers

def main():
    img = cv2.imread(sprite)
    spriteHeight, spriteWidth = img.shape[:2]
    baseCharWidth = spriteWidth / len(chars)
    uvWidth = 1 / len(chars)

    f = open ("fontMakingToolOut.txt", "w")
    writeHeader(f, lineSpacing + spriteHeight)

    currentChar = 0
    for char in chars:
        charIndex = ord(char)
        uvs = [currentChar * uvWidth, 0, uvWidth, 1]
        verts = [0, 0, baseCharWidth, spriteHeight]
        advance = baseCharWidth + characterSpacing
        for j in range(int(baseCharWidth)):
            col = int(currentChar * baseCharWidth + j)
            hasPixel = False
            for line in range(spriteHeight):
                color = img[line][col]
                if color[0] != 0 or color[1] != 0 or color[2] != 0:
                    hasPixel = True
            if hasPixel:
                break
            else:
                uvs[0] += 1 / spriteWidth
                uvs[2] -= 1 / spriteWidth
                advance -= 1
        for j in range(int(baseCharWidth)):
            col = int(currentChar * baseCharWidth + (baseCharWidth - 1 - j))
            hasPixel = False
            for line in range(spriteHeight):
                color = img[line][col]
                if color[0] != 0 or color[1] != 0 or color[2] != 0:
                    hasPixel = True
            if hasPixel:
                break
            else:
                uvs[2] -= 1 / spriteWidth
                advance -= 1
        verts[2] = advance - 1
        writeCharDataToFile(f, charIndex, uvs, verts, advance)
        currentChar += 1

    writeSpacebarChar(f, spriteHeight)
    writeFooter(f)
    f.close()

def writeHeader(file, lineSpacing):
    file.write("%YAML 1.1\n")
    file.write("%TAG !u! tag:unity3d.com,2011:\n")
    file.write("--- !u!128 &12800000\n")
    file.write("Font:\n")
    file.write("  m_ObjectHideFlags: 0\n")
    file.write("  m_CorrespondingSourceObject: {fileID: 0}\n")
    file.write("  m_PrefabInstance: {fileID: 0}\n")
    file.write("  m_PrefabAsset: {fileID: 0}\n")
    file.write("  m_Name: ActionButtonFont\n")
    file.write("  serializedVersion: 5\n")
    file.write("  m_LineSpacing: " + str(lineSpacing) + "\n")
    file.write("  m_DefaultMaterial: {fileID: 2100000, guid: 148c5a18c10fa274fa31b13a636f7903, type: 2}\n")
    file.write("  m_FontSize: 0\n")
    file.write("  m_Texture: {fileID: 0}\n")
    file.write("  m_AsciiStartOffset: 0\n")
    file.write("  m_Tracking: 1\n")
    file.write("  m_CharacterSpacing: 0\n")
    file.write("  m_CharacterPadding: 1\n")
    file.write("  m_ConvertCase: 1\n")
    file.write("  m_CharacterRects:\n")

def writeCharDataToFile(file, charIndex, uvs, verts, advance):
    verts[2] *= fontScaling
    verts[3] *= fontScaling
    advance *= fontScaling
    file.write("  - serializedVersion: 2\n")
    file.write("    index: " + str(charIndex) + "\n")
    file.write("    uv:\n")
    file.write("      serializedVersion: 2\n")
    file.write("      x: " + str(uvs[0]) + "\n")
    file.write("      y: " + str(uvs[1]) + "\n")
    file.write("      width: " + str(uvs[2]) + "\n")
    file.write("      height: " + str(uvs[3]) + "\n")
    file.write("    vert:\n")
    file.write("      serializedVersion: 2\n")
    file.write("      x: " + str(verts[0]) + "\n")
    file.write("      y: " + str(verts[1]) + "\n")
    file.write("      width: " + str(verts[2]) + "\n")
    file.write("      height: -" + str(verts[3]) + "\n")
    file.write("    advance: " + str(advance) + "\n")
    file.write("    flipped: 0\n")

def writeSpacebarChar(file, spriteHeight):
    writeCharDataToFile(file, ord(' '), [0, 0, 0, 1], [0, 0, 0, spriteHeight], spacebarWidth)

def writeFooter(file):
    file.write("  m_KerningValues: []\n")
    file.write("  m_PixelScale: 0.1\n")
    file.write("  m_FontData:\n")
    file.write("  m_Ascent: 0\n")
    file.write("  m_Descent: 0\n")
    file.write("  m_DefaultStyle: 0\n")
    file.write("  m_FontNames: []\n")
    file.write("  m_FallbackFonts: []\n")
    file.write("  m_FontRenderingMode: 0\n")
    file.write("  m_UseLegacyBoundsCalculation: 0\n")
    file.write("  m_ShouldRoundAdvanceValue: 1\n")

if __name__ == '__main__':
    main()