#include "cpifonti.h"

#include <string.h>


bool cpifont_get_next_entry(
  const cpifont_stream       *s,
        cpifont_entry_info   *entry)
{
  bool ret;
  cpi_file_header file_hdr;
  cpi_codepage_entry_header entry_header;
  cpi_font_data_header data_header;

  if (entry->next_offset == 0) {
    ret = _get_file_header(s, &file_hdr);

    if (!ret) {
      return false;
    }

    if (!_matches_tag(file_hdr.file_tag, cpi_file_tag)) {
      return false;
    }

    s->seek(
      s->context,
      file_hdr.offset + sizeof(cpi_font_info_header),
      CPIFONT_ORIGIN_BEG);
  } else {
    s->seek(s->context, entry->next_offset, CPIFONT_ORIGIN_BEG);
  }

  s->read(s->context, (char*)&entry_header, sizeof(entry_header.header_size));
  switch (entry_header.header_size) {
    case sizeof(entry_header):
      s->read(
        s->context,
        ((char*)&entry_header) + sizeof(entry_header.header_size),
        sizeof(entry_header) - sizeof(entry_header.header_size));
      break;

    case sizeof(entry_header) - 2:
      s->read(
        s->context,
        ((char*)&entry_header) + sizeof(entry_header.header_size),
        sizeof(entry_header) - 2 - sizeof(entry_header.header_size));
      entry_header.font_offset &= 0xFFFF;
      break;

    default: return false;
  }

  switch (entry_header.device_type) {
    case CPI_DEVICE_DISPLAY:
      entry->device_type = CPIFONT_DEVICE_TYPE_DISPLAY;
      break;

    case CPI_DEVICE_PRINTER:
      entry->device_type = CPIFONT_DEVICE_TYPE_PRINTER;
      break;
    
    default:
      entry->device_type = CPIFONT_DEVICE_TYPE_UNKNOWN;
      break;
  }

  if (_matches_tag(entry_header.device_name, cpi_device_name_ega)) {
    entry->device = CPIFONT_DEVICE_EGA;
  } else if (_matches_tag(entry_header.device_name, cpi_device_name_lcd)) {
    entry->device = CPIFONT_DEVICE_LCD;
  } else if (_matches_tag(entry_header.device_name, cpi_device_name_cga)) {
    entry->device = CPIFONT_DEVICE_CGA;
  } else if (_matches_tag(entry_header.device_name, cpi_device_name_mono)) {
    entry->device = CPIFONT_DEVICE_MONO;
  } else {
    entry->device = CPIFONT_DEVICE_UNKNOWN;
  }
  memcpy(
    entry->device_name,
    entry_header.device_name,
    sizeof(entry_header.device_name));
  entry->device_name[8] = 0;

  entry->codepage = entry_header.codepage;

  s->seek(s->context, entry_header.font_offset, CPIFONT_ORIGIN_BEG);
  s->read(s->context, (char*)&data_header, sizeof(data_header));

  entry->fonts = data_header.fonts_count;
  entry->fonts_offset = entry_header.font_offset + sizeof(data_header);
  entry->fonts_size = data_header.data_length;

  entry->next_offset = entry_header.next_offset;
  return true;
}
