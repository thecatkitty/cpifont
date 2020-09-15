#include "cpifonti.h"

#include <string.h>


bool cpifont_is_cpi(
  const cpifont_stream       *s)
{
  size_t pos;
  bool ret;
  cpi_file_header file_hdr;

  pos = s->tell(s->context);
  ret = _get_file_header(s, &file_hdr);
  s->seek(s->context, pos, CPIFONT_ORIGIN_BEG);

  if (!ret) {
    return false;
  }

  return _matches_tag(file_hdr.file_tag, cpi_file_tag);
}

int  cpifont_get_entry_count(
  const cpifont_stream       *s)
{
  size_t pos;
  bool ret;
  size_t count;
  cpi_file_header file_hdr;
  cpi_font_info_header info_hdr;

  pos = s->tell(s->context);
  ret = _get_file_header(s, &file_hdr);
  if (!ret) {
    return -1;
  }

  s->seek(s->context, file_hdr.offset, CPIFONT_ORIGIN_BEG);
  count = s->read(s->context, (char*)&info_hdr, sizeof(cpi_font_info_header));
  if (count != sizeof(cpi_font_info_header)) {
    return -1;
  }

  s->seek(s->context, pos, CPIFONT_ORIGIN_BEG);
  return info_hdr.entries_count;
}
