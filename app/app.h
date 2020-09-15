#pragma once

#include <cpifont.h>

#include <string>


extern cpifont_stream cfs;


int cmd_dump(
        bool                 print_glyphs);
int cmd_glyph(
        int                  eidx,
        int                  fidx,
        int                  gidx);

void print_entry(
        cpifont_entry_info   &entry,
  const std::string          head);
void print_font(
  const cpifont_entry_info   &entry,
        cpifont_font_info    &font,
  const std::string          head);
void print_glyph(
  const cpifont_font_info    &font,
        size_t               index,
  const std::string          head);
