cmake_minimum_required(VERSION 3.10)

project("cpitool" C CXX)

include_directories(cpifont/inc)
include_directories("${CMAKE_BINARY_DIR}/cpifont")

add_subdirectory(cpifont)
add_subdirectory(cpitool/cpp)
add_subdirectory(cpitool/cs)
add_subdirectory(Celones.CpiFont)
