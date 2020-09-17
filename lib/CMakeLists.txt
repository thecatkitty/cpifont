project("cpifont" C)

include(GenerateExportHeader)

include_directories("${CMAKE_CURRENT_BINARY_DIR}")

add_library(${PROJECT_NAME} SHARED
    common.c
    header.c
    entries.c
    fonts.c
    names.c
    lib.h
    cpifont_exports.h
)

GENERATE_EXPORT_HEADER(${PROJECT_NAME}
    BASE_NAME          ${PROJECT_NAME}
    EXPORT_MACRO_NAME  CPIFONT_EXPORTS
    EXPORT_FILE_NAME   cpifont_exports.h
    STATIC_DEFINE      SHARED_EXPORTS_BUILT_AS_STATIC
)
