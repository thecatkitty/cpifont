#include "app.hpp"

#include <bitset>
#include <iostream>


static std::bitset<32> to_bitset(
  const char                 *bytes,
        size_t               count)
{
  std::bitset<32> ret{};

  for (size_t i = 0; i < count; i++) {
    ret[i] = (bytes[i / 8] >> (7 - (i % 8))) & 1;
  }

  return std::move(ret);
}

void print_entry(
        cpifont_entry_info   &entry,
  const std::string          head)
{
  std::cout << head << "device       = "
            << cpifont_get_device_string(entry.device) << std::endl;
  std::cout << head << "device type  = "
            << cpifont_get_device_type_string(entry.device_type)
            << std::endl;
  std::cout << head << "device name  = "
            << entry.device_name << std::endl;
  std::cout << head << "codepage     = "
            << entry.codepage << std::endl;
  std::cout << head << "fonts        = "
            << entry.fonts << std::endl;
  std::cout << head << "fonts offset = "
            << entry.fonts_offset << std::endl;
  std::cout << head << "fonts size   = "
            << entry.fonts_size << std::endl;
}

void print_font(
  const cpifont_entry_info   &entry,
        cpifont_font_info    &font,
  const std::string          head)
{
  std::cout << head << "glyph size    = "
            << (int)font.glyph_width << 'x'
            << (int)font.glyph_height << std::endl;
  std::cout << head << "glyphs        = "
            << font.glyphs << std::endl;
  std::cout << head << "bitmap offset = "
            << font.bitmap_offset << std::endl;
  std::cout << head << "bitmap size   = "
            << font.bitmap_size << std::endl;
}

void print_glyph(
  const cpifont_font_info    &font,
        size_t               index,
  const std::string          head)
{
  char glyph[32];
  cpifont_get_glyph(&cfs, &font, index, glyph);

  std::cout << head;
  auto row_size = (font.glyph_width - 1) / 8 + 1;
  for (int r = 0; r < font.glyph_height; r++) {
    auto bits = to_bitset(glyph + r * row_size, font.glyph_width);
    for (int c = 0; c < font.glyph_width; c++) {
      std::cout.put(bits[c] ? '#' : ' ');
    }
    std::cout << std::endl;
  }
}