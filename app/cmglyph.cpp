#include "app.hpp"

#include <iostream>


int cmd_glyph(
        int                  eidx,
        int                  fidx,
        int                  gidx)
{
  int entries;
  cpifont_get_entry_count(&cfs, &entries);

  if (eidx >= entries) {
    std::cerr << "error: entry index out of range" << std::endl;
    return 21;
  }

  cpifont_entry_info entry{0};
  for (int e = 0; e <= eidx; e++) {
    cpifont_get_next_entry(&cfs, &entry);
  }

  if (fidx >= entry.fonts) {
    std::cerr << "error: font index out of range" << std::endl;
    return 22;
  }

  cpifont_font_info font{};
  for (int f = 0; f <= fidx; f++) {
    cpifont_get_next_font(&cfs, &entry, &font);
  }

  if (gidx >= font.glyphs) {
    std::cerr << "error: glyph index out of range" << std::endl;
    return 23;
  }

  print_glyph(font, gidx, "");
  return 0;
}
