#include "app.hpp"

#include <fstream>
#include <iostream>


int main(
        int                  argc,
        char                 *argv[])
{
  if (argc <= 1) {
    std::cerr << "error: no file name provided" << std::endl;
    return 1;
  }

  std::ifstream fin{argv[1], std::ios::binary};
  if (!fin.good()) {
    std::cerr << "error: cannot open '" << argv[1] << "'" << std::endl;
    return 2;
  }

  cfs.context = &fin;

  if (!cpifont_is_cpi(&cfs)) {
    std::cerr << "error: not a CPI file" << std::endl;
    return 3;
  }

  switch (argc) {
    case 2:
      return cmd_dump(false);

    case 3:
      return cmd_dump(std::string{argv[2]} == "-G");

    case 5:
      return cmd_glyph(std::atoi(argv[2]), std::atoi(argv[3]), std::atoi(argv[4]));

    default:
      std::cerr << "error: invalid number of arguments" << std::endl;
      return 4;
  }

  return 0;
}
