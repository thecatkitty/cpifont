#include <algorithm>
#include <bitset>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <vector>

#include <cpi.h>
#include <cpifont.h>


std::bitset<32> to_bitset(
  const char                 *bytes,
        size_t               count)
{
  std::bitset<32> ret{};

  for (size_t i = 0; i < count; i++) {
    ret[i] = (bytes[i / 8] >> (7 - (i % 8))) & 1;
  }

  return std::move(ret);
}


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

  cpifont_stream cfs{
    /*read*/[](void *ctx, char *buffer, size_t bytes) -> size_t {
      auto fi = reinterpret_cast<std::istream*>(ctx);
      try {
        fi->read(buffer, bytes);
        return bytes;
      } catch(...) {
        return 0;
      }
    },
    /*write*/nullptr,
    /*flush*/nullptr,
    /*tell*/[](void *ctx) -> size_t {
      auto fi = reinterpret_cast<std::istream*>(ctx);
      return fi->tellg();
    },
    /*seek*/[](void *ctx, size_t offset, cpifont_origin origin) -> bool {
      auto fi = reinterpret_cast<std::istream*>(ctx);
      try {
        auto way =
          origin == CPIFONT_ORIGIN_BEG ? std::ios::beg :
          origin == CPIFONT_ORIGIN_CUR ? std::ios::cur : std::ios::end;
        fi->seekg(offset, way);
        return true;
      } catch(...) {
        return false;
      }
    },
    /*context*/&fin
  };

  if (!cpifont_is_cpi(&cfs)) {
    std::cerr << "error: not a CPI file" << std::endl;
    return 3;
  }

  auto entries = cpifont_get_entry_count(&cfs);
  std::cout << "number of entries : " << entries << std::endl;

  cpifont_entry_info entry{0};
  for (int e = 0; e < entries; e++) {
    cpifont_get_next_entry(&cfs, &entry);

    std::cout << "entry " << e << ": device       = "
              << cpifont_get_device_string(entry.device) << std::endl;
    std::cout << "entry " << e << ": device type  = "
              << cpifont_get_device_type_string(entry.device_type)
              << std::endl;
    std::cout << "entry " << e << ": device name  = "
              << entry.device_name << std::endl;
    std::cout << "entry " << e << ": codepage     = "
              << entry.codepage << std::endl;
    std::cout << "entry " << e << ": fonts        = "
              << entry.fonts << std::endl;
    std::cout << "entry " << e << ": fonts offset = "
              << entry.fonts_offset << std::endl;
    std::cout << "entry " << e << ": fonts size   = "
              << entry.fonts_size << std::endl;

    for (int f = 0; f < entry.fonts; f++) {
      cpifont_font_info font{};
      cpifont_get_next_font(&cfs, &entry, &font);

      std::cout << "font " << e << '.' << f << ": glyph size    = "
                << (int)font.glyph_width << 'x'
                << (int)font.glyph_height << std::endl;
      std::cout << "font " << e << '.' << f << ": glyphs        = "
                << font.glyphs << std::endl;
      std::cout << "font " << e << '.' << f << ": bitmap offset = "
                << font.bitmap_offset << std::endl;
      std::cout << "font " << e << '.' << f << ": bitmap size   = "
                << font.bitmap_size << std::endl;

      auto row_size = (font.glyph_width - 1) / 8 + 1;
      for (int g = 0; g < font.glyphs; g++) {
        std::cout << "glyph " << e << '.' << f << '.' << g << std::endl;

        char glyph[32];
        cpifont_get_glyph(&cfs, &font, g, glyph);

        for (int r = 0; r < font.glyph_height; r++) {
          auto bits = to_bitset(glyph + r * row_size, font.glyph_width);
          for (int c = 0; c < font.glyph_width; c++) {
            std::cout.put(bits[c] ? '#' : ' ');
          }
          std::cout << std::endl;
        }
      }
    }
  }

  return 0;
}
