project("cpitool" CXX)

include_directories("${CMAKE_SOURCE_DIR}/inc")
include_directories("${CMAKE_BINARY_DIR}/lib")

add_executable(${PROJECT_NAME}
    cpitool.cpp
    cmdump.cpp
    cmglyph.cpp
    print.cpp
    stream.cpp
)

target_link_libraries(${PROJECT_NAME} cpifont)
