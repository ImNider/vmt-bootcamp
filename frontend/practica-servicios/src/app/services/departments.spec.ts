import { TestBed } from '@angular/core/testing';

import { DepartamentsService } from './departments';

describe('Departaments', () => {
  let service: DepartamentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartamentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
