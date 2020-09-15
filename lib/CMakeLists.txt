include(GenerateExportHeader)

include_directories("${CMAKE_CURRENT_BINARY_DIR}")

add_library(cpifont SHARED
    common.c
    header.c
    entries.c
    fonts.c
    names.c
    lib.h
    cpifont_exports.h
)

GENERATE_EXPORT_HEADER(cpifont
    BASE_NAME          cpifont
    EXPORT_MACRO_NAME  CPIFONT_EXPORTS
    EXPORT_FILE_NAME   cpifont_exports.h
    STATIC_DEFINE      SHARED_EXPORTS_BUILT_AS_STATIC
)

set_target_properties(cpifont PROPERTIES
    RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_BINARY_DIR}/.."
)
