#include "app.h"

#include <fstream>
#include <iostream>
#include <sstream>


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

  auto entries = cpifont_get_entry_count(&cfs);
  std::cout << "number of entries : " << entries << std::endl;

  cpifont_entry_info entry{0};
  for (int e = 0; e < entries; e++) {
    cpifont_get_next_entry(&cfs, &entry);

    std::ostringstream oss;
    oss << "entry " << e << ": ";
    print_entry(entry, oss.str());

    for (int f = 0; f < entry.fonts; f++) {
      cpifont_font_info font{};
      cpifont_get_next_font(&cfs, &entry, &font);

      std::ostringstream oss;
      oss << "font " << e << '.' << f << ": ";
      print_font(entry, font, oss.str());

      for (int g = 0; g < font.glyphs; g++) {
        std::ostringstream oss;
        oss << "glyph " << e << '.' << f << '.' << g << std::endl;
        print_glyph(font, g, oss.str());
      }
    }
  }

  return 0;
}
