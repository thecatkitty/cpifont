#include "lib.h"

#include <string.h>


bool _get_file_header(
  const cpifont_stream       *s,
        cpi_file_header      *header)
{
  size_t count;

  count = s->read(s->context, (char*)header, sizeof(cpi_file_header));

  return count == sizeof(cpi_file_header);
}

bool _matches_tag(
  const char                 *str,
  const char                 *tag)
{
  return memcmp(str, tag, 8) == 0;
}
