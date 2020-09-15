#include <algorithm>
#include <bitset>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <vector>


#pragma pack(push, r1, 1)
struct cpi_file_header {
  char     file_tag[8];
  uint64_t _reserved;
  uint16_t pointers_count;
  int8_t   pointer_type;
  uint32_t offset;
};

struct cpi_font_info_header {
  uint16_t entries_count;
};

struct cpi_codepage_entry_header {
  uint16_t header_size;
  uint32_t next_offset;
  uint16_t device_type;
  char     device_name[8];
  uint16_t codepage;
  uint16_t _reserved[3];
  uint32_t font_offset;
};

struct cpi_font_data_header {
  uint16_t _reserved;
  uint16_t fonts_count;
  uint16_t data_length;
};

struct cpi_font_data {
  uint8_t  character_rows;
  uint8_t  character_cols;
  uint16_t _aspect_ratio;
  uint16_t characters_count;
  char     data[0];
};
#pragma pack(pop, r1)

const char cpi_file_tag[8] = {0xFF, 'F', 'O', 'N', 'T', ' ', ' ', ' '};

enum class cpi_device_type : uint16_t {
  display = 1,
  printer = 2
};

const char cpi_device_name_cga[8] = {'C', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_ega[8] = {'E', 'G', 'A', ' ', ' ', ' ', ' ', ' '};
const char cpi_device_name_mono[8] = {'M', 'O', 'N', 'O', ' ', ' ', ' ', ' '};
const char cpi_device_name_lcd[8] = {'L', 'C', 'D', ' ', ' ', ' ', ' ', ' '};


std::vector<char> read_stream_contents(std::istream& istr) {
  std::vector<char> buff;
  istr.seekg(0, std::ios::end);
  buff.resize(istr.tellg());
  istr.seekg(0, std::ios::beg);
  istr.read(buff.data(), buff.size());
  return std::move(buff);
}

std::bitset<32> to_bitset(const char* bytes, size_t count) {
  std::bitset<32> ret{};

  for (size_t i = 0; i < count; i++) {
    ret[i] = (bytes[i / 8] >> (7 - (i % 8))) & 1;
  }

  return std::move(ret);
}


int main(int argc, char* argv[]) {
  if (argc <= 1) {
    std::cerr << "error: no file name provided" << std::endl;
    return 1;
  }

  std::ifstream fin{argv[1], std::ios::binary};
  if (!fin.good()) {
    std::cerr << "error: cannot open '" << argv[1] << "'" << std::endl;
    return 2;
  }

  auto file_data = read_stream_contents(fin);

  cpi_file_header* file_hdr{reinterpret_cast<cpi_file_header*>(
    file_data.data())};
  if (!std::equal(file_hdr->file_tag, file_hdr->file_tag + 8, cpi_file_tag)) {
    std::cerr << "error: no CPI file signature" << std::endl;
    return 3;
  }

  std::cout << "file: pointers count = "
            << file_hdr->pointers_count << std::endl;
  std::cout << "file: pointer type   = "
            << (int)file_hdr->pointer_type << std::endl;
  std::cout << "file: info offset    = "
            << file_hdr->offset << std::endl;
  std::cout << std::endl;

  auto info_hdr = reinterpret_cast<cpi_font_info_header*>
    (file_data.data() + file_hdr->offset);
  std::cout << "info: entries count = "
            << info_hdr->entries_count << std::endl;
  std::cout << std::endl;

  auto entry_hdr = reinterpret_cast<cpi_codepage_entry_header*>
    (file_data.data() + file_hdr->offset + sizeof(uint16_t));
  for (unsigned i = 0; i < info_hdr->entries_count; i++) {
    std::cout << "entry " << i << ": header size = "
              << entry_hdr->header_size << std::endl;
    std::cout << "entry " << i << ": next offset = "
              << entry_hdr->next_offset << std::endl;
    std::cout << "entry " << i << ": device type = "
              << entry_hdr->device_type << std::endl;
    std::cout << "entry " << i << ": device name = "
              << entry_hdr->device_name[0]
              << entry_hdr->device_name[1]
              << entry_hdr->device_name[2]
              << entry_hdr->device_name[3]
              << entry_hdr->device_name[4]
              << entry_hdr->device_name[5]
              << entry_hdr->device_name[6]
              << entry_hdr->device_name[7] << std::endl;
    std::cout << "entry " << i << ": codepage    = "
              << entry_hdr->codepage << std::endl;
    std::cout << "entry " << i << ": font offset = "
              << entry_hdr->font_offset << std::endl;
    
    auto data_hdr = reinterpret_cast<cpi_font_data_header*>
      (file_data.data() + entry_hdr->font_offset);
    std::cout << "data " << i << ": fonts count = "
              << data_hdr->fonts_count << std::endl;
    std::cout << "data " << i << ": data length = "
              << data_hdr->data_length << std::endl;

    auto font_data = reinterpret_cast<cpi_font_data*>(data_hdr + 1);
    for (unsigned j = 0; j < data_hdr->fonts_count; j++) {
      std::cout << "font " << i << ',' << j << ": dimensions = "
                << (unsigned)font_data->character_cols << 'x'
                << (unsigned)font_data->character_rows << std::endl;
      std::cout << "font " << i << ',' << j << ": characters = "
                << font_data->characters_count << std::endl;

      auto row_size = (font_data->character_cols - 1) / 8 + 1;
      auto glyph_size = row_size * font_data->character_rows;
      
      for (unsigned k = 0; k < font_data->characters_count; k++) {
        std::cout << "glyph " << i << ',' << j << ',' << k << std::endl;
        for (unsigned row = 0; row < font_data->character_rows; row++) {
          auto bits = to_bitset(
            font_data->data + k * glyph_size + row * row_size,
            font_data->character_cols);
          for (unsigned bit = 0; bit < font_data->character_cols; bit++) {
            std::cout.put(bits[bit] ? '#' : ' ');
          }
          std::cout << std::endl;
        }
      }
      
      font_data = reinterpret_cast<cpi_font_data*>
        ((char*)font_data + font_data->characters_count * glyph_size);
    }

    std::cout << std::endl;

    entry_hdr = reinterpret_cast<cpi_codepage_entry_header*>
    (file_data.data() + entry_hdr->next_offset);
  }

  return 0;
}
