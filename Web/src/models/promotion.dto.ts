import { CustomerDto } from "./customer.dto";
import { EmployeePackageDto } from "./employee-package.dto";

export interface PromotionDto {
  id?: string;
  customerId?: string;
  customer?: CustomerDto;
  employeePackages?: EmployeePackageDto[];
}
