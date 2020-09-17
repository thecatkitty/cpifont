find_program(DOTNET_COMMAND dotnet)

function(add_dotnet_project)
    set(options)
    set(oneValueArgs TARGETNAME)
    set(multiValueArgs BYPRODUCTS DEPENDS)
    cmake_parse_arguments(DOTNET_PROJECT
        "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN})

    list(APPEND DOTNET_PROJECT_BYPRODUCTS "${DOTNET_PROJECT_TARGETNAME}.deps.json")
    list(APPEND DOTNET_PROJECT_BYPRODUCTS "${DOTNET_PROJECT_TARGETNAME}.runtimeconfig.json")
    list(APPEND DOTNET_PROJECT_BYPRODUCTS "${DOTNET_PROJECT_TARGETNAME}.runtimeconfig.dev.json")

    add_custom_command(
        OUTPUT "${CMAKE_CURRENT_BINARY_DIR}/${DOTNET_PROJECT_TARGETNAME}.dll"
        COMMAND ${DOTNET_COMMAND} build "${DOTNET_PROJECT_TARGETNAME}.csproj" -o "${CMAKE_CURRENT_BINARY_DIR}" -v q
        DEPENDS ${DOTNET_PROJECT_DEPENDS}
        BYPRODUCTS ${DOTNET_PROJECT_BYPRODUCTS}
        WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    )

    add_custom_target(${DOTNET_PROJECT_TARGETNAME} ALL DEPENDS "${CMAKE_CURRENT_BINARY_DIR}/${DOTNET_PROJECT_TARGETNAME}.dll")
endfunction()

add_dotnet_project(
    TARGETNAME CpiTool
    BYPRODUCTS "CommandLine.dll" "cpifont.dll"
)
