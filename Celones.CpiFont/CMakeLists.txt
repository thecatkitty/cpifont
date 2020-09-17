project("Celones.CpiFont")

include(../dotnet.cmake)

add_dotnet_project(
    TARGETNAME ${PROJECT_NAME}
    BYPRODUCTS "cpifont.dll"
)
