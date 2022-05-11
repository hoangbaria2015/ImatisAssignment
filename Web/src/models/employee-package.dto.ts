import { PackageDto } from "./package.dto";

export interface EmployeePackageDto {
  id?: string;
  packageId?: string;
  package?: PackageDto;
  amount?: number;
  quantity?: number;
}