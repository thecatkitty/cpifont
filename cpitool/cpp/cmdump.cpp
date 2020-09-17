#include "cpitool.hpp"

#include <iostream>
#include <sstream>


int cmd_dump(
        bool                 print_glyphs)
{
  cpifont_type type;
  cpifont_get_type(&cfs, &type);
  std::cout << "file: type    = "
            << cpifont_get_type_string(type) << std::endl;

  int entries;
  cpifont_get_entry_count(&cfs, &entries);
  std::cout << "file: entries = "
            << entries << std::endl;

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
        if (print_glyphs) {
          print_glyph(font, g, oss.str());
        }
      }
    }
  }

  return 0;
}
