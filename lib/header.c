#include "lib.h"

#include <string.h>


cpifont_status cpifont_get_type(
  const cpifont_stream       *s,
        cpifont_type         *type)
{
  cpifont_status status;
  size_t pos;
  cpi_file_header file_header;

  pos = s->tell(s->context);
  status = s->read(s->context, (char*)&file_header, sizeof(cpi_file_header));
  s->seek(s->context, pos, CPIFONT_ORIGIN_BEG);

  if (status != CPIFONT_OK) {
    *type = CPIFONT_TYPE_UNKNOWN;
    return status;
  }

  if (_matches_tag(file_header.file_tag, cpi_dos_file_tag)) {
    *type = CPIFONT_TYPE_DOS;
    return CPIFONT_OK;
  }
  
  if (_matches_tag(file_header.file_tag, cpi_nt_file_tag)) {
    *type = CPIFONT_TYPE_NT;
    return CPIFONT_OK;
  }

  *type = CPIFONT_TYPE_UNKNOWN;
  return CPIFONT_OK;
}

cpifont_status cpifont_get_entry_count(
  const cpifont_stream       *s,
        int                  *entry_count)
{
  cpifont_status status;
  size_t pos;
  cpi_file_header file_header;
  cpi_font_info_header info_hdr;

  pos = s->tell(s->context);
  status = s->read(s->context, (char*)&file_header, sizeof(cpi_file_header));
  if (status != CPIFONT_OK) {
    return status;
  }

  s->seek(s->context, file_header.offset, CPIFONT_ORIGIN_BEG);
  status = s->read(s->context, (char*)&info_hdr, sizeof(cpi_font_info_header));
  if (status != CPIFONT_OK) {
    return status;
  }

  s->seek(s->context, pos, CPIFONT_ORIGIN_BEG);
  *entry_count = info_hdr.entries_count;
  return CPIFONT_OK;
}
