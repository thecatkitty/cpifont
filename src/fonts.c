#include "cpifonti.h"


bool cpifont_get_next_font(
  const cpifont_stream       *s,
  const cpifont_entry_info   *entry,
        cpifont_font_info    *font)
{
  size_t end, pos;
  cpi_font_data data;
  int row_size, glyph_size;

  end = entry->fonts_offset + entry->fonts_size;
  pos = s->tell(s->context);
  if (pos == end) {
    return false;
  } else if (pos < entry->fonts_offset || pos > end) {
    s->seek(s->context, entry->fonts_offset, CPIFONT_ORIGIN_BEG);
  }

  s->read(s->context, (char*)&data, sizeof(data));
  row_size = (data.character_cols - 1) / 8 + 1;
  glyph_size = row_size * data.character_rows;

  font->glyph_width = data.character_cols;
  font->glyph_height = data.character_rows;
  font->glyphs = data.characters_count;
  font->bitmap_offset = s->tell(s->context);
  font->bitmap_size = glyph_size * data.characters_count;
  return true;
}

bool cpifont_get_glyph(
  const cpifont_stream       *s,
  const cpifont_font_info    *font,
        size_t               index,
        char                 *glyph)
{
  int row_size, glyph_size;
  size_t pos, count;

  if (index >= font->glyphs) {
    return false;
  }

  row_size = (font->glyph_width - 1) / 8 + 1;
  glyph_size = row_size * font->glyph_height;

  pos = s->tell(s->context);
  s->seek(
    s->context,
    font->bitmap_offset + glyph_size * index,
    CPIFONT_ORIGIN_BEG);
  count = s->read(s->context, glyph, glyph_size);
  if (count != glyph_size) {
    return false;
  }

  s->seek(s->context, pos, CPIFONT_ORIGIN_BEG);
  return true;
}
