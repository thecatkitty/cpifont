project("CpiTool")

include(../../dotnet.cmake)

add_dotnet_project(
    TARGETNAME ${PROJECT_NAME}
    DEPENDS Celones.CpiFont
    BYPRODUCTS "CommandLine.dll"
)
