using System.Reflection;
using FUBusiness.Models;

namespace FUCourseManagement.Helpers
{
    public static class PropertyHelper
    {
        public static string GetDisplayValue(object item, PropertyInfo prop)
        {
            if (item == null || prop == null)
                return "";

            var value = prop.GetValue(item);
            if (value == null)
                return "";

            // Xử lý collection
            if (value is System.Collections.IEnumerable && value.GetType().IsGenericType)
            {
                var collection = ((System.Collections.IEnumerable)value).Cast<object>();
                var formattedItems = collection.Select(x =>
                {
                    if (x == null)
                        return "";
                    // In tất cả thuộc tính của từng phần tử trong collection
                    var properties = x.GetType().GetProperties();
                    var propValues = properties.Select(p =>
                        $"{p.Name}: {p.GetValue(x)?.ToString() ?? ""}"
                    );
                    return "{" + string.Join(", ", propValues) + "}";
                });
                return string.Join("; ", formattedItems);
            }

            // Nếu là navigation property
            if (prop.PropertyType.Namespace == "FUBusiness.Models")
            {
                // Lấy property chính để hiển thị
                var displayProp = prop
                    .PropertyType.GetProperties()
                    .FirstOrDefault(p =>
                        p.Name is "FullName" or "Title" or "DisplayName" or "Name"
                    );
                return displayProp?.GetValue(value)?.ToString() ?? value.ToString();
            }

            // Xử lý các kiểu dữ liệu thông thường
            if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
            {
                var dateValue = value as DateTime?;
                return dateValue?.ToString("dd/MM/yyyy HH:mm") ?? "";
            }

            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                return (value as bool?) == true ? "Có" : "Không";
            }

            return value.ToString();
        }

        public static string GetDisplayName(PropertyInfo prop)
        {
            // Nếu là Id của navigation property (UserId, CourseId)
            if (prop.Name.EndsWith("Id") && !prop.Name.Equals("Id"))
            {
                var navigationPropName = prop.Name.Substring(0, prop.Name.Length - 2);
                return navigationPropName;
            }

            // Map tên thuộc tính sang tiếng Việt
            string displayName = prop.Name switch
            {
                "Title" => "Tiêu đề",
                "Name" => "Tên",
                "FullName" => "Họ tên",
                "Category" => "Danh mục",
                "Capacity" => "Sức chứa",
                "CreatedAt" => "Ngày tạo",
                "EnrollDate" => "Ngày đăng ký",
                "Dropped" => "Đã hủy",
                "User" => "Người dùng",
                "Email" => "Email",
                "Role" => "Vai trò",
                "UserId" => "Người dùng",
                _ => prop.Name,
            };

            // Nếu là collection thì thêm "Danh sách" vào trước
            if (
                prop.PropertyType.IsGenericType
                && prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
            )
            {
                var elementType = prop.PropertyType.GetGenericArguments()[0];
                displayName = $"Danh sách {displayName}";
            }

            return displayName;
        }

        public static bool IsNavigationProperty(PropertyInfo prop)
        {
            return prop.PropertyType.Namespace == "FUBusiness.Models";
        }

        public static bool IsNavigationId(PropertyInfo prop)
        {
            return prop.Name.EndsWith("Id") && !prop.Name.Equals("Id");
        }
    }
}
